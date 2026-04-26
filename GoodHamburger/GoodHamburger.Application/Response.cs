namespace GoodHamburger.Application;

public class Response<T>
{
    public bool IsSucess { get;  private set; }
    public T? Data { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public string? Error { get; private set; }

    protected Response()
    {
    }
    
    public static Response<T> Ok(T data, string message = "") =>
        new Response<T>
        {
            IsSucess =  true,
            Data =  data,
            Message =  message,
        };
    
    public static Response<T> Fail(string message, string error) =>
        new Response<T>
        {
            IsSucess =  false,
            Message =  message,
            Error =  error,
        };
}