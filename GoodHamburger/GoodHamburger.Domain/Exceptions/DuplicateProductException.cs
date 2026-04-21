using GoodHamburger.Domain.Constants;

namespace GoodHamburger.Domain.Exceptions;

public class DuplicateProductException : DomainException
{

    public DuplicateProductException() : base(ErrorCodes.DuplicateProduct, ErrorMessages.DuplicateProduct)
    {
    }
}