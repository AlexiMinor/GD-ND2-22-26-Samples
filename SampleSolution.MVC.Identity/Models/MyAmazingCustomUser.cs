using Microsoft.AspNetCore.Identity;

namespace SampleSolution.MVC.Identity.Models;

public class MyAmazingCustomUser : IdentityUser<long>
{
    public string MyAmazingCustomProperty { get; set; }
}