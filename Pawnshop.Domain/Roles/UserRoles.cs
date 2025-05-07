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

    // Priviliges to Clients
    public const string ClientsDisplay = nameof(ClientsDisplay);
    public const string ClientsDisplayAndModify = nameof(ClientsDisplayAndModify);

    // Priviliges to Workplaces
    public const string WorkplaceDisplay = nameof(WorkplaceDisplay);
    public const string WorkplaceDisplayAndModify = nameof(WorkplaceDisplayAndModify);

    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>()
        {
            new(HeadAdmin),

            new(UsersDisplay),
            new(UsersDisplayAndModify),

            new(EmployeesDisplay),
            new(EmployeesDisplayAndModify),

            new(ClientsDisplay),
            new(EmployeesDisplayAndModify),

            new(WorkplaceDisplay),
            new(WorkplaceDisplayAndModify)
        };
    }
    
    public static List<IdentityRole<Guid>> GetRoles() => Roles;
}