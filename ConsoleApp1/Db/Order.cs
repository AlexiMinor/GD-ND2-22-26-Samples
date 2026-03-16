namespace ConsoleProjectWithDb.Db;

public class Order 
{
    public int Id { get; set; } //Id or ClassNameId(OrderId) is primary key by convention
    public string ProductName { get; set; }
    public decimal Price { get; set; }

    public int UserId { get; set; } // Foreign key by convention, it should be ClassNameId (UserId)
                                    // to be recognized as foreign key by convention
    public User User { get; set; } // Navigation property, it should be ClassName (User) to be recognized as navigation property by convention

    public List<OrderItem> OrderItems { get; set; } = [];
}