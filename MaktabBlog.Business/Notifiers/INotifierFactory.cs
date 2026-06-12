using MaktabBlog.ExternalServices.Notifiers;

namespace MaktabBlog.Business.Notifiers;

public interface INotifierFactory
{
    INotifier? GetNotifier();
}