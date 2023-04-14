using System.Net;

namespace proyecto.API.Middleware.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
