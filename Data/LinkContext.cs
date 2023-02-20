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

    }

}
