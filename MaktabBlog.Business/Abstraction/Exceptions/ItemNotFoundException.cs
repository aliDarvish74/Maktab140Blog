using MaktabBlog.Domain;

namespace MaktabBlog.Business.Abstraction.Exceptions;

public class ItemNotFoundException : BaseBusinessException
{
    public ItemNotFoundException(string itemName,Type type,  Exception? innerException = null) 
        : base($"{itemName} not found.", $"{type.Name}_404", innerException)
    {
    }
}