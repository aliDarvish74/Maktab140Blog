namespace MaktabBlog.WebAPI.Models.Abstractions;

public class GeneralDto
{
    public GeneralDto(Guid resourceId)
    {
        ResourceId = resourceId;
    }
    public Guid ResourceId { get; set; }
}