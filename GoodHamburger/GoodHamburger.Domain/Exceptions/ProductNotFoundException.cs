using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public class ProductNotFoundException : DomainException
{

    public ProductNotFoundException() : base(ErrorCodes.ProductNotFound, ErrorMessages.ProductNotFound)
    {
    }
}