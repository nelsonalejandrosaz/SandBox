using Microsoft.AspNetCore.Identity;

namespace SandBox.Models;

public class AppRole : IdentityRole
{
    public ICollection<Policy> Policies { get; set; }
}