using MaktabBlog.Business.Abstraction.Exceptions;
using MaktabBlog.Domain.Users;

namespace MaktabBlog.Business.Users.Exceptions;

public class UserNotFoundException : ItemNotFoundException
{
    public UserNotFoundException(string itemName, Exception? innerException = null) 
        : base(itemName, typeof(User), innerException)
    {
    }
}