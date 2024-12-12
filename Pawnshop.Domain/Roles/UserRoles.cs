using Microsoft.AspNetCore.Identity;

namespace Pawnshop.Domain.Roles;

public static class UserRoles
{
    public const string Szef = nameof(Szef);
    public const string Kierownik = nameof(Kierownik);
    public const string Pracownik = nameof(Pracownik);

    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>()
        {
            new(Szef),
            new(Kierownik),
            new(Pracownik)
        };
    }
    
    public static List<IdentityRole<Guid>> GetRoles() => Roles;
}