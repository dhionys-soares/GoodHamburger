using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public sealed class DiscountCannotBeNullException : DomainException
{

    public DiscountCannotBeNullException() : base(ErrorCodes.DiscountCannotBeNull, ErrorMessages.DiscountCannotBeNull)
    {
    }
}