using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace MaktabBlog.ExternalServices.Inquiries;

public class InquiryService : IInquiryService
{
    private readonly IDistributedCache _cache;
    private readonly IOptionsMonitor<InquiryConfiguration> _optionsMonitor;

    public InquiryService(
        IDistributedCache cache,
        IOptionsMonitor<InquiryConfiguration> optionsMonitor)
    {
        _cache = cache;
        _optionsMonitor = optionsMonitor;
    }
    public async Task<bool> IsAvailableAsync()
    {
        var cachedData = await _cache.GetStringAsync("InquiryService:Availability");
        
        if(cachedData != null)
            return bool.Parse(cachedData);
        
        var task = Task.Delay(5000);
        
        var result =  _optionsMonitor.CurrentValue.IsAvailable;
        await task;

        await _cache.SetStringAsync("InquiryService:Availability", result.ToString(), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
        });

        return result;
    }
}