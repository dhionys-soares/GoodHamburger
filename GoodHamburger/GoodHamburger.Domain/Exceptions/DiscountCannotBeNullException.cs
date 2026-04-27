using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public class DiscountCannotBeNullException : DomainException
{

    public DiscountCannotBeNullException() : base(ErrorCodes.DiscountCannotBeNull, ErrorMessages.DiscountCannotBeNull)
    {
    }
}