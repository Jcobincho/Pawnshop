using Microsoft.AspNetCore.Identity;

namespace Pawnshop.Domain.Roles;

public static class UserRoles
{
    public const string Boss = nameof(Boss);
    public const string Manager = nameof(Manager);
    public const string Worker = nameof(Worker);

    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>()
        {
            new(Boss),
            new(Manager),
            new(Worker)
        };
    }
    
    public static List<IdentityRole<Guid>> GetRoles() => Roles;
}