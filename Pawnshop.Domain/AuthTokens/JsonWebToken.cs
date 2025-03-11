namespace Pawnshop.Domain.AuthTokens;

public class JsonWebToken
{
    public string AccessToken { get; init; }
    public RefreshToken RefreshToken { get; set; }
    public long Expires { get; set; }
    public Guid UserId { get; set; }
    public ICollection<string> Roles { get; set; } = new List<string>();
    public Dictionary<string, string> Claims { get; init; } = new Dictionary<string, string>();
}