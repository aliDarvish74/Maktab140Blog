using MaktabBlog.ExternalServices.Notifiers;

namespace MaktabBlog.Business.Notifiers;

public class NotifierConfiguration
{
    public bool IsActive { get; set; }
    public NotifierTypes NotifierType { get; set; }
}