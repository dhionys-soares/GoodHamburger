using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public sealed class InvalidProductPriceException : DomainException
{

    public InvalidProductPriceException() : base(ErrorCodes.InvalidProductPrice, ErrorMessages.InvalidProductPrice)
    {
    }
}