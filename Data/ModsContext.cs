using Microsoft.EntityFrameworkCore;
using shortenyour.link.Models;
using shortenyour.link.Models;

namespace shortenyour.link.Data
{
    public class ModsContext : DbContext
    {
        public ModsContext(DbContextOptions<ModsContext> options)
      : base(options)
        {
        }

        public DbSet<Mods> mods { get; set; }


    }
}
