using Pawnshop.Domain.Abstract;

namespace Pawnshop.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public string ErrorMessage { get; }
    
    public BadRequestException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}