namespace MaktabBlog.ExternalServices.Notifiers;

public class SmsNotifier : INotifier
{
    public NotifierTypes GetNotifierType() => NotifierTypes.Sms;

    public void Send(string message)
    {
        Console.WriteLine($"[SMS notifier] [Message: {message}] [SENT]");
    }
}