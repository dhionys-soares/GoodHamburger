namespace GoodHamburger.Web.Responses;

public class ResponseModel<T>
{
    public bool IsSucess { get; set; }
    
    public T? Data { get; set; }
    
    public string Message { get; set; }
    
    public string? Error { get; set; }
}