namespace GoodHamburger.Domain.Constants;

public static class ErrorCodes
{
    public const string ProductCannotBeNull = "PRODUCT_CANNOT_BE_NULL";
    public const string InvalidQuantity = "INVALID_QUANTITY";
    public const string DuplicateProduct = "DUPLICATE_PRODUCT";
    public const string InvalidProductName = "INVALID_PRODUCT_NAME";
    public const string InvalidProductPrice = "INVALID_PRODUCT_PRICE";
    public const string DiscountOutOfRangeAllowed = "DISCOUNT_OUT_OF_RANGE_ALLOWED";
    public const string DiscountCannotBeNull = "DISCOUNT_CANNOT_BE_NULL";
    public const string ProductNotFound = "PRODUCT_NOT_FOUND";
}