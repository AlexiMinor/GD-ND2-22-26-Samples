using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.Db.Entities;

namespace SampleSolution.MVC.Identity.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser<long>, 
    IdentityRole<long>, 
    long>(options)
{
    public DbSet<Article> Articles { get; set; }

    
}
