using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public sealed class DiscountOutOfRangeAllowedException : DomainException
{

    public DiscountOutOfRangeAllowedException() : base(ErrorCodes.DiscountOutOfRangeAllowed, ErrorMessages.DiscountOutOfRangeAllowed)
    {
    }
}