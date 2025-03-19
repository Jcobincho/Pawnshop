using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;
using Pawnshop.Domain.Roles;

namespace Pawnshop.Infrastructure.Persistance.Seeders;

public class BossAccountSeeder
{
    private const string Email = "jakisemail@gmail.com";
    private const string Password = "ZAQ!2wsx";
    private const string Name = "Imie";
    private const string Surname = "Nazwisko";

    internal static async Task SeedBossAccountAsync(UserManager<Users> userManager, DbContext dbContext, CancellationToken cancellationToken = default)
    {
        var isUserExist = await userManager.Users.AnyAsync(x => x.Email == Email, cancellationToken);
        
        if(isUserExist) return;
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var bossUser = new Users()
        {
            Email = Email,
            UserName = Email,
        };
        
        var createUser = await userManager.CreateAsync(bossUser, Password);
        if (!createUser.Succeeded) throw new CreateUserException(createUser.Errors);

        var addBossRole = await userManager.AddToRoleAsync(bossUser, UserRoles.HeadAdmin);
        if (!addBossRole.Succeeded) throw new BadRequestException("You cannot add boss role");
        
        var addEmail = await userManager.AddClaimAsync(bossUser, new Claim(ClaimTypes.Email, Email));
        if (!addEmail.Succeeded) throw new BadRequestException("You cannot add email claim");

        var addIdentifier =
            await userManager.AddClaimAsync(bossUser, new Claim(ClaimTypes.NameIdentifier, bossUser.Id.ToString()));
        if (!addIdentifier.Succeeded) throw new BadRequestException("You cannot add boss identifier");
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
    }
}