using MaktabBlog.Business.Abstraction.Exceptions;

namespace MaktabBlog.Business.Users.Exceptions;

public class UserRegistrationException : BaseBusinessException
{
    public UserRegistrationException(string message) 
        : base(message, "User_1")
    {
    }
}