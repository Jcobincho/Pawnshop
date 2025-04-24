using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;
using Pawnshop.Domain.Entitie;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure;

public class DbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IUserClaimsDataProviderService _userClaimsDataProviderService;
    public DbContext(DbContextOptions<DbContext> options, IUserClaimsDataProviderService userClaimsDataProviderService) : base(options)
    {
        _userClaimsDataProviderService = userClaimsDataProviderService;
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveConversion<DateTimeToUtcConverter>();

        //base.ConfigureConventions(configurationBuilder);
    }

    private class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeToUtcConverter() : base
            (
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            ) 
        {  
        
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            UpdateBaseEntity();
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new BadRequestException("You are trying to edit a mismatched record.");
        }
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

// Add-Migration init -Project Pawnshop.Infrastructure -StartupProject Pawnshop.Api