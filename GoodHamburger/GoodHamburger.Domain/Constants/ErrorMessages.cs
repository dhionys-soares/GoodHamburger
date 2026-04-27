namespace GoodHamburger.Domain.Constants;

public class ErrorMessages
{
    public const string ProductCannotBeNull = "Product cannot be null.";
    public const string InvalidQuantity = "Quantity must be greater than zero.";
    public const string DuplicateProduct = "Cannot add an item with the same type.";
    public const string InvalidProductName = "Product name cannot be null or whitespace.";
    public const string InvalidProductPrice = "Product price must be greater than zero.";
    public const string DiscountOutOfRangeAllowed = "Discount cannot be out of  range allowed.";
    public const string DiscountCannotBeNull = "Discount cannot be null.";
    public const string ProductNotFound = "Product not found in Order";
}