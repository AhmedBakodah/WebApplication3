using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Banner> Banner { get; set; } = default!;
        public DbSet<WebApplication1.Models.Client> Client { get; set; } = default!;
        public DbSet<WebApplication1.Models.Event> Event { get; set; } = default!;
    }
}
