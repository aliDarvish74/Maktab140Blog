using System.Runtime.Serialization;

namespace MaktabBlog.ExternalServices.Notifiers;

public enum NotifierTypes
{
    [EnumMember(Value = "Email")]
    Email = 0,
    [EnumMember(Value = "Sms")]
    Sms = 1
}