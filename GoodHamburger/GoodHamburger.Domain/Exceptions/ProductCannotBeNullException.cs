using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public sealed class ProductCannotBeNullException : DomainException
{

    public ProductCannotBeNullException() : base(ErrorCodes.ProductCannotBeNull, ErrorMessages.ProductCannotBeNull)
    {
    }
}