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
    public const string WorkplacesDisplay = nameof(WorkplacesDisplay);
    public const string WorkplacesDisplayAndModify = nameof(WorkplacesDisplayAndModify);

    // Priviliges to ItemCategories
    public const string ItemCategoriesDisplay = nameof(ItemCategoriesDisplay);
    public const string ItemCategoriesDisplayAndModify = nameof(ItemCategoriesDisplayAndModify);

    // Priviliges to ItemDetails
    public const string ItemDetailsDisplay = nameof(ItemDetailsDisplay);
    public const string ItemDetailsDisplayAndModify = nameof(ItemDetailsDisplayAndModify);

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
            new(ClientsDisplayAndModify),

            new(WorkplacesDisplay),
            new(WorkplacesDisplayAndModify),

            new(ItemCategoriesDisplay),
            new(ItemCategoriesDisplayAndModify),

            new(ItemDetailsDisplay),
            new(ItemDetailsDisplayAndModify)
        };
    }
    
    public static List<IdentityRole<Guid>> GetRoles() => Roles;
}