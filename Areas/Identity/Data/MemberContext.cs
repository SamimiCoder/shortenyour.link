using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shortenyour.link.Areas.Identity.Data;

namespace shortenyour.link.Data;

public class MemberContext : IdentityDbContext<shortenyourlinkUser>
{
    public MemberContext(DbContextOptions<MemberContext> options)
        : base(options)
    {
    }
    public DbSet<shortenyourlinkUser> users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ShortenyourlinkuserEntity());
    }
}

internal class ShortenyourlinkuserEntity : IEntityTypeConfiguration<shortenyourlinkUser>
{
    public void Configure(EntityTypeBuilder<shortenyourlinkUser> builder)
    {
        builder.Property(u => u.Name).HasMaxLength(256);
        builder.Property(u => u.LastName).HasMaxLength(256);

        builder.Property(u => u.CardNo).HasMaxLength(256);

    }
}