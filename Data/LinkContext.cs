using Microsoft.EntityFrameworkCore;
using shortenyour.link.Models;

namespace shortenyour.link.Data
{

    public class LinkContext : DbContext
    {
        public LinkContext(DbContextOptions<LinkContext> options)
       : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Link>(entity =>
            {
                entity.Property(e => e.LinkBalance).HasColumnType("DECIMAL(18,2)");
            });
        }

    }

}
