namespace Pawnshop.Domain.Entities
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
        public bool IsExpired => DateTimeOffset.UtcNow >= Expires;

        public UserRefreshToken()
        {

        }

        private UserRefreshToken(string token, DateTimeOffset expires)
        {
            Token = token;
            Expires = expires;
        }

        public static UserRefreshToken Create(string token, DateTimeOffset expires) => new(token, expires);
    }
}
