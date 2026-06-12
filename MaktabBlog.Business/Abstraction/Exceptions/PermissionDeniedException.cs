using MaktabBlog.Domain;

namespace MaktabBlog.Business.Abstraction.Exceptions;

public class PermissionDeniedException : BaseBusinessException
{
    public PermissionDeniedException(Exception? innerException = null) 
        : base("Permission denied. You can't access this resource.", "403", innerException)
    {
    }
}