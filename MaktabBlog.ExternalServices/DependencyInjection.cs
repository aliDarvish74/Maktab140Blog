using MaktabBlog.ExternalServices.Inquiries;
using MaktabBlog.ExternalServices.Notifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaktabBlog.ExternalServices;

public static class DependencyInjection
{
    public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotifier, EmailNotifier>();
        services.AddScoped<INotifier, SmsNotifier>();
        services.AddScoped<IInquiryService, InquiryService>();
        services.Configure<InquiryConfiguration>(configuration.GetSection("InquiryConfiguration"));
    }
}