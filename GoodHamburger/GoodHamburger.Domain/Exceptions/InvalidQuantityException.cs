using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public sealed class InvalidQuantityException : DomainException
{

    public InvalidQuantityException() : base(ErrorCodes.InvalidQuantity, ErrorMessages.InvalidQuantity)
    {
    }
}