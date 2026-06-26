namespace MaktabBlog.ExternalServices.Inquiries;

public interface IInquiryService
{
    Task<bool> IsAvailableAsync();
}