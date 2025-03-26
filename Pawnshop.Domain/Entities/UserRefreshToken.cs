using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Domain.Entities
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.Now >= Expires;

        public UserRefreshToken()
        {
            
        }

        private UserRefreshToken(string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }

        public static UserRefreshToken Create(string token, DateTime expires) => new(token, expires);
    }
}
