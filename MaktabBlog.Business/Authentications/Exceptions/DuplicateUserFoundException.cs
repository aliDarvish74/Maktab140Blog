using MaktabBlog.Business.Abstraction.Exceptions;

namespace MaktabBlog.Business.Authentications.Exceptions;

public class DuplicateUserFoundException : BaseBusinessException
{
    public DuplicateUserFoundException(string key) : base(
        $"Duplicate user found with key: {key}", "User_409")
    {
    }
}