using System.Diagnostics;
using ConsoleProjectWithDb.Db;
using Microsoft.EntityFrameworkCore;

namespace ConsoleProjectWithDb
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
          
            //IQueryable<User> users = null;
            using (var dbContext = new TestDbContext())
            {
                await CreateALotOfData(dbContext);
                //await AddUsersAsync(dbContext);

                //await AddUserWithOrderAsync(dbContext);

                //await AddOrderWithUserAsync(dbContext);
                //await GetUserSamples(dbContext);
                //await ReadUsers(dbContext);
                //await CreateSampleData(dbContext);
                //await GetUserSamples(dbContext);
                //await ChangeTrackingSample(dbContext);
                //users = GetUsersWithOrders(dbContext);

                //await UpdateUserAsync(dbContext);
                //await UpdateUser2Async(dbContext);

            }
      
            //foreach (var user in users) // actual place to request data from db
            //{
            //    Console.WriteLine(user.Email);
            //}
        }


        private static async Task ChangeTrackingSample(TestDbContext dbContext)
        {
            var newUser = new User
            {
                Id = 0,
                Email = "SomeEmail@em.ail",
                PasswordHash = "123",
                Address = "null",
            };

            var entry = await dbContext.Users.AddAsync(newUser);


            //var user = await dbContext.Users.FirstOrDefaultAsync();
            //var userEntry = dbContext.Entry<User>(user); //state - unchanged


            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var userEntry = dbContext.Entry<User>(user);
            userEntry.Entity.Email = "Changed email";
            userEntry.State = EntityState.Modified;//save changes => UPDATE Users SET Email = 'Changed email' WHERE Id = user.Id
            var x = 0;
        }

        private static async Task CreateSampleData(TestDbContext dbContext)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Price = 10.5m,
                Name = "Test product #1"
            };
            var order = new Order()
            {
                Price = 10.5m,
                ProductName = "Test product #1",
            };

            var oiList = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Product = product,
                    Quantity = 5
                },
                new OrderItem()
                {
                    Product = product,
                    Quantity = 10
                }
            };
            order.OrderItems = oiList;
            var user = new User()
            {
                Email = "SampleUser@email.org",
                PasswordHash = "Some password hash",
                Address = "Sample address #1111",
                Orders = [order]
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        //READ

        private static async Task UpdateUserAsync(TestDbContext dbContext)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "F.Baggins@test.com");
            if (user != null)
            {
                user.Address = "New address #1111";
            }

            await dbContext.SaveChangesAsync();
        }

        //works but it is not recommended to use Update method if the entity is already tracked by the context,
        private static async Task UpdateUser2Async(TestDbContext dbContext)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == 14);
            user.Address = "New address #2222";

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }


        //CRUD 
        // Create - Add, AddRange, AddAsync, AddRangeAsync

        // Update 
        // Delete - Remove, RemoveRange
        private static async Task AddUsersAsync(TestDbContext dbContext)
        {
            List<User> users =
            [
                new User
                {
                    Email = "JohnDoe@test.com", Address = "Test address #1", PasswordHash = "Some password hash"
                },
                new User
                {
                    Email = "B.Baggins@test.com", Address = "Test address #1", PasswordHash = "Some password hash"
                }
            ];
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }

        private static async Task AddUserWithOrderAsync(TestDbContext dbContext)
        {
            var userWithOrders = new User
            {
                Email = "Test@email.com",
                Address = "Test address #3",
                PasswordHash = "Some password hash",
                Orders =
                [
                    new Order() { Price = 2.5m, ProductName = "Test product #1" },
                    new Order() { Price = 3.5m, ProductName = "Test product #2" },
                    new Order() { Price = 4.5m, ProductName = "Test product #3" }
                ]
            };

            await dbContext.Users.AddAsync(userWithOrders);
            await dbContext.SaveChangesAsync();
        }

        private static async Task AddOrderWithUserAsync(TestDbContext context)
        {
            var user = new User
            {
                Email = "F.Baggins@test.com",
                Address = "Test address #15",
                PasswordHash = "Some password hash"
            };
            var userEntry = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var order1 = new Order() { Price = 3.5m, ProductName = "Test product #15", UserId = userEntry.Entity.Id };
            var order2 = new Order() { Price = 5.5m, ProductName = "Test product #122", User = user };

            await context.Orders.AddRangeAsync(order1, order2);
            await context.SaveChangesAsync();
        }

        // Read 
        private static async Task ReadUsers(TestDbContext dbContext)
        {
            var usersFromDb = await dbContext.Users
                .AsNoTracking()
                .Include(u => u.Orders)
                .ToArrayAsync();

            foreach (var user in usersFromDb)
            {
                Console.WriteLine(
                    $"{user.Id}-{user.Email} has orders for total sum of $ {user.Orders?.Sum(order => order.Price) ?? 0}");
            }
        }
        private static async Task GetUserSamples(TestDbContext dbContext)
        {
            var usersToSelect = await dbContext.Users
                .AsNoTrackingWithIdentityResolution()
                .Where(user => user.Email.EndsWith(".org"))
                .OrderBy(user => user.Email)
                .ThenBy(user => user.Id)
                .ToArrayAsync();
            Console.WriteLine(usersToSelect.Length);

            var usersToFind = await dbContext.Users.FindAsync(3);
            //var userToGetById = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == 3);
            var userToGetById = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == 3);

            //projection 
            var userProjections = await dbContext.Users
                .Select(u => new { Id = u.Id, Email = u.Email, Address = u.Address })
                .ToArrayAsync();

            foreach (var userProjection in userProjections)
            {
                Console.WriteLine($"{userProjection.Id} - {userProjection.Email} - {userProjection.Address}");
            }


            var usersWithOrderData = await dbContext.Users
                .Include(u => u.Orders) // Get Nav property for User (or root level object)
                    .ThenInclude(o => o.OrderItems) // Get Nav property for Order (or first level object)
                        .ThenInclude(oi => oi.Product)
                .Select(user => new
                {
                    Id = user.Id,
                    Sum = user.Orders.SelectMany(order => order.OrderItems).Select(item => item.Quantity * item.Product.Price)
                        .Sum()
                })
                .ToArrayAsync(); // Get Nav property for OrderItem (or second level object)

            foreach (var user in usersWithOrderData)
            {
                Console.WriteLine($"{user.Id} - {user.Sum}");
            }


            var oi = dbContext.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(order => order.User)
                .Include(oi => oi.Product);

            //not for our case but for some cases it can be useful 
            // let's imagine that oi have some additional navProperty
            //var complexInclude = dbContext.Users
            //    .Include(u=>u.Orders)
            //        .ThenInclude(order => order.OrderItems)
            //            .ThenInclude(oi => oi.Product)
            //    .Include(u => u.Orders)
            //        .ThenInclude(order => order.OrderItems)
            //                .ThenInclude(oi => oi.Order)//some additional nav-property for OrderItem
            //    .ToArray();




            //var usersWithOrderDataArray = await usersWithOrderData.ToArrayAsync();


        }

        // IQueryable is a query that has not been executed yet, 
        // it is just an expression tree that can be further modified before execution
        // IQueryable will be executed only when we try to enumerate it (e.g. with foreach)
        // or when we call a method that forces execution (e.g. ToList, ToArray, FirstOrDefault, etc.)
        private static IQueryable<User> GetUsersWithOrders(TestDbContext dbContext)
        {
            return dbContext.Users.AsQueryable(); // no call has been made to the database yet,
            // we just have a queryable object that can be further modified before execution
        }

        private static IReadOnlyCollection<User> BadSampleForNeverRepeat(TestDbContext dbContext)
        {
            //dbContext.Users - 5M records in the database
            var userArray1 = dbContext.Users
                .ToList()
                .Where(user => string.IsNullOrEmpty(user.PasswordHash))
                .ToArray()
                .AsReadOnly();
            //sql to get userArray1: SELECT * FROM Users => we get all 5M records from the database and then filter them in memory

            var userArray2 = dbContext.Users
                .Where(user => string.IsNullOrEmpty(user.PasswordHash))
                .ToArray()
                .AsReadOnly();
            //sql to get userArray2: SELECT *
            //FROM Users
            //WHERE PasswordHash IS NULL OR PasswordHash = ''


            //var newUser = new User();
            //var newUser2 = new User();

            //var t1 = dbContext.Users.AddRangeAsync(newUser, newUser2);
            //var t2 = dbContext.Users.Where(user => string.IsNullOrEmpty(user.PasswordHash)).ToArrayAsync();
            //var t3 = Task.Run(() =>
            //{
            //    foreach (var u in userArray1)
            //    {
            //        u.PasswordHash = "";
            //    }
            //});

            //Task.WaitAll([t1, t2, t3]);




            return userArray2;



        }

        private static async Task<int> GetUsersCountAsync(TestDbContext dbContext)
        {
            return await dbContext.Users.CountAsync(); //Count & CountAsync wil be executed immediately 
        }

        private static IEnumerable<Product> GetGeneratedProducts(int productCount)
        {
            var products = new List<Product>(productCount);

            for (int i = 1; i <= productCount; i++)
            {
                products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product #{i}",
                    Price = decimal.Round((decimal)(new Random().Next(1000, 100000)) / 100, 2)
                });
            }

            return products;
        }

        private static async Task CreateALotOfData(TestDbContext context)
        {
            var products = GetGeneratedProducts(1000000); //100k, 1M 

            //v4 - 30 sec
            var sw = new Stopwatch();
            sw.Start();
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
            sw.Stop();
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }
    }
}
