namespace MaktabBlog.WebAPI.Models.Abstractions;

public class BaseResponseDto<TData>
{
    public TData? Data { get; set; }
    public bool IsSuccess { get; set; }
    public BaseError? Error { get; set; }
}

public class BaseError
{
    public string Code { get; set; }
    public string Message { get; set; }
}