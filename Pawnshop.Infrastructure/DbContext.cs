using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;
using Pawnshop.Domain.Entities;

namespace Pawnshop.Infrastructure;

public class DbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IUserClaimsDataProviderService _userClaimsDataProviderService;
    public DbContext(DbContextOptions<DbContext> options, IUserClaimsDataProviderService userClaimsDataProviderService) : base(options)
    {
        _userClaimsDataProviderService = userClaimsDataProviderService;
    }
    
    public DbSet<Employees> Employee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        UpdateBaseEntity();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateBaseEntity()
    {
        var entires = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach(var entry in entires)
        {
            if(entry.Entity is BaseEntity entity)
            {
                var userId = _userClaimsDataProviderService.GetUserIdFromClaims();
                entity.SetBaseEntity(userId);
            }
        }
    }
}