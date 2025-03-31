using System.Net;

namespace Pawnshop.Web.Exceptions
{
    public class ApiException : Exception
    {
        public List<string> ErrorMessages { get;}
        public HttpStatusCode StatusCode { get;}

        public ApiException(List<string> errorMessages, HttpStatusCode statusCode) : base($"API Error ({statusCode}): {string.Join(", ", errorMessages)}")
        {
            ErrorMessages = errorMessages;
            StatusCode = statusCode;
        }
    }
}
