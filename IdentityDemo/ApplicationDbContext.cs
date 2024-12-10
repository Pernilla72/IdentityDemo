using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityDemo
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) // Kalla basens konstruktor med options
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Behövs när vi ärver från IdentityDbContext...
            base.OnModelCreating(modelBuilder);
        }
    }
}
