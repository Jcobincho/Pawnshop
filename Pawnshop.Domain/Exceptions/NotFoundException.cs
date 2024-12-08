using Pawnshop.Domain.Abstract;

namespace Pawnshop.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public string ErrorMessage { get; }
    
    public NotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}