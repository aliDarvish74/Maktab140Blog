namespace MaktabBlog.ExternalServices.Notifiers;

public interface INotifier
{
    public NotifierTypes GetNotifierType();
    public void Send(string message);
}