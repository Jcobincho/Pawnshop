using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawnshop.Domain.AuthTokens;

namespace Pawnshop.Domain.Entities;

public class Users : IdentityUser<Guid>
{
    public Guid? EmployeesId { get; set; } = new Guid();

    public IReadOnlyCollection<UserRefreshToken> RefreshToken => _refreshToken;

    private List<UserRefreshToken> _refreshToken = new();

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        var token = UserRefreshToken.Create(refreshToken.Token, refreshToken.Expires);
        _refreshToken.Add(token);
    }

    public void DeleteRefreshToken(UserRefreshToken refreshToken)
    {
        _refreshToken.Remove(refreshToken);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        
    }
}