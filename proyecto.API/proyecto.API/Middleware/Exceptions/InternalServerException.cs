using System.Net;

namespace proyecto.API.Middleware.Exceptions
{
    public class InternalServerException : CustomException
    {
        public InternalServerException(string message, List<string>? errors = default)
            : base(message, errors, HttpStatusCode.InternalServerError) { }
    }
}
