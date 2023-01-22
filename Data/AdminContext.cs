using Microsoft.EntityFrameworkCore;
using shortenyour.link.Models;

namespace shortenyour.link.Data
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options)
      : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }

    }
}
