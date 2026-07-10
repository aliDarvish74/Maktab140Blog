using System.Security.Claims;

namespace MaktabBlog.Business.Authentications.Constants;

public class ClaimConstants
{
    public static readonly Claim VipUser = new ("Subscription", "Vip"); 
    public static readonly Claim MightyHand = new ("MightyHand", "true"); 
}