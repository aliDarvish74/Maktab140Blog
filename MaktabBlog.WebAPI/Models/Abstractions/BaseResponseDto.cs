using System.Text.Json.Serialization;

namespace MaktabBlog.WebAPI.Models.Abstractions;

public class BaseResponseDto<TData>
{
    [JsonIgnore(Condition =  JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; set; }
    public bool IsSuccess { get; set; }
    
    [JsonIgnore(Condition =  JsonIgnoreCondition.WhenWritingNull)]
    public BaseError? Error { get; set; }

    public BaseResponseDto(TData data)
    {
        Data = data;
        IsSuccess = true;
    }

    /// <summary>
    /// Generate a failed response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="code">Service generated error code</param>
    public BaseResponseDto(string message, string code)
    {
        IsSuccess = false;
        Error = new BaseError
        {
            Code = code,
            Message = message
        };
    }
}

public class BaseError
{
    public string Code { get; set; }
    public string Message { get; set; }
}