using DiskApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DiskApp.Models;

namespace DiskApp.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<AppUser, Role, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Disk> Disks { get; set; }  
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AppUserEntityConfiguration());
        builder.ApplyConfiguration(new RoleEntityConfiguration());
        builder.ApplyConfiguration(new RoleUserConfiguration());
        base.OnModelCreating(builder);
    }
}

public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var hasher = new PasswordHasher<AppUser>();
        builder.HasData(new AppUser()
        {
            Id = 1,
            Email = "admin@site.com",
            FirsName = "Admin",
            LastName = "Adminstration",
            PasswordHash = hasher.HashPassword(null, "Qwerty123!"),
            NormalizedUserName = "ADMIN@SITE.COM",
            NormalizedEmail = "ADMIN@SITE.COM",
            EmailConfirmed = true,
            UserName = "admin@site.com",
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString()
        },
        new AppUser()
        {
            Id = 2,
            Email = "user@site.com",
            FirsName = "User",
            LastName = "User",
            PasswordHash = hasher.HashPassword(null, "Qwerty123!"),
            NormalizedUserName = "USER@SITE.COM",
            NormalizedEmail = "USER@SITE.COM",
            EmailConfirmed = true,
            UserName = "user@site.com",
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString()
        });
    }
}

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role()
        {
            Id = 1,
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new()
        {
            Id = 2,
            Name = "User",
            NormalizedName = "USER"
        });
    }
}

public class RoleUserConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasData(new IdentityUserRole<int>()
        {
            RoleId = 1,
            UserId = 1,
        },
        new()
        {
            RoleId = 2,
            UserId = 2,
        });
    }
}