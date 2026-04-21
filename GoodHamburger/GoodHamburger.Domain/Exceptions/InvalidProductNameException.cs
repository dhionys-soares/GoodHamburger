using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public sealed class InvalidProductNameException : DomainException
{

    public InvalidProductNameException() : base(ErrorCodes.InvalidProductName,  ErrorMessages.InvalidProductName)
    {
    }
}