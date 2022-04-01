using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SandBox.Models;

namespace SandBox.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<Producto> Productos { get; set; }

    }
}