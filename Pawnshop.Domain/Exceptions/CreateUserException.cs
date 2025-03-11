using Microsoft.AspNetCore.Identity;
using Pawnshop.Domain.Abstract;

namespace Pawnshop.Domain.Exceptions;

public class CreateUserException : BaseException
{
    public CreateUserException() : base("Password is incorrect.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CreateUserException(IEnumerable<IdentityError> errors) : this()
    {
        Errors = errors.GroupBy(x => x.Code, x => x.Description)
            .ToDictionary(x => x.Key, x => x.ToArray());
    }
    
    public IDictionary<string, string[]> Errors { get; set; }
}