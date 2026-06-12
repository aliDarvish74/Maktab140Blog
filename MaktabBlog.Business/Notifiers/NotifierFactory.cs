using MaktabBlog.ExternalServices.Notifiers;
using Microsoft.Extensions.Options;

namespace MaktabBlog.Business.Notifiers;

public class NotifierFactory : INotifierFactory
{
    private readonly IEnumerable<INotifier> _notifiers;
    private readonly IOptionsMonitor<List<NotifierConfiguration>> _options;

    public NotifierFactory(IEnumerable<INotifier>  notifiers, IOptionsMonitor<List<NotifierConfiguration>> options)
    {
        _notifiers = notifiers;
        _options = options;
    }
    public INotifier? GetNotifier()
    {
        var activeNotifier = _options.CurrentValue.FirstOrDefault(n => n.IsActive);
        
        if (activeNotifier == null)
            throw new Exception("No active notifier");
        
        return _notifiers.FirstOrDefault(n => n.GetNotifierType() == activeNotifier.NotifierType);
    }
}