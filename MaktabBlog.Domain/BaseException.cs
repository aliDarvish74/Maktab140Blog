namespace MaktabBlog.Domain;

public abstract class BaseException : Exception
{
    protected BaseException(string message, string code, Exception? innerException = null) 
        : base(message, innerException)
    {
        Code = code;
    }
    public string Code { get; private set; }
}