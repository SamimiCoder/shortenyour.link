using System;
using Microsoft.EntityFrameworkCore;
using shortenyour.link.Models;

namespace shortenyour.link.Data
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options)
            : base(options)
        {
        }
        public DbSet<Notification> Notifications { get; set; }
    }
}
