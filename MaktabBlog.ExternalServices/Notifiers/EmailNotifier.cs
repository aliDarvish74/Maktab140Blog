namespace MaktabBlog.ExternalServices.Notifiers;

public class EmailNotifier : INotifier
{
    public NotifierTypes GetNotifierType() => NotifierTypes.Email;

    public void Send(string message)
    {
        Console.WriteLine($"[Email notifier] [Message: {message}] [SENT]");
    }
}