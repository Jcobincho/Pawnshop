namespace Pawnshop.Domain.AuthTokens;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}