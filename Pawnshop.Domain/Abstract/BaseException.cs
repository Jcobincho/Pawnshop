namespace Pawnshop.Domain.Abstract;

public abstract class BaseException : Exception
{
    protected BaseException(string message) : base(message) { }
}