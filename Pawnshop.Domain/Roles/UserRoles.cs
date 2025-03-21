using Microsoft.AspNetCore.Identity;

namespace Pawnshop.Domain.Roles;

public static class UserRoles
{
    // HeadAdmin has every priviliges 
    public const string HeadAdmin = nameof(HeadAdmin);

    // Priviliges to Users account
    public const string UsersDisplay = nameof(UsersDisplay);
    public const string UsersDisplayAndModify = nameof(UsersDisplayAndModify);

    // Priviliges to Employees
    public const string EmployeesDisplay = nameof(EmployeesDisplay);
    public const string EmployeesDisplayAndModify = nameof(EmployeesDisplayAndModify);

    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>()
        {
            new(HeadAdmin),

            new(UsersDisplay),
            new(UsersDisplayAndModify),

            new(EmployeesDisplay),
            new(EmployeesDisplayAndModify)
        };
    }
    
    public static List<IdentityRole<Guid>> GetRoles() => Roles;
}