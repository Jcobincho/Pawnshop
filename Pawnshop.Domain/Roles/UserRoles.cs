using Microsoft.AspNetCore.Identity;

namespace Pawnshop.Domain.Roles;

public static class UserRoles
{
    public const string HeadAdmin = nameof(HeadAdmin);

    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>()
        {
            new(HeadAdmin),
        };
    }
    
    public static List<IdentityRole<Guid>> GetRoles() => Roles;
}