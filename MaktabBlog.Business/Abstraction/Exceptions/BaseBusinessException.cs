using MaktabBlog.Domain;

namespace MaktabBlog.Business.Abstraction.Exceptions;

public class BaseBusinessException : BaseException
{
    public BaseBusinessException(string message, string code, Exception? innerException = null) 
        : base(message, $"BusinessException_{code}", innerException)
    {
    }
}