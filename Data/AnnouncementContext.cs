using Microsoft.EntityFrameworkCore;
using shortenyour.link.Models;

namespace shortenyour.link.Data
{
    public class AnnouncementContext : DbContext
    {
        public AnnouncementContext(DbContextOptions<AnnouncementContext> options)
      : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
    }
}
