using GoodHamburger.Application.Constants;

namespace GoodHamburger.Application.Exceptions;

public class RequestCannotBeNullException : ApplicationException
{

    public RequestCannotBeNullException() : base(ErrorCodes.RequestCannotBeNull, ErrorMessages.RequestCannotBeNull)
    {
    }
}