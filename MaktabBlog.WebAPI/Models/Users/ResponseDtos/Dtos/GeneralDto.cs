namespace MaktabBlog.WebAPI.Models.Users.ResponseDtos.Dtos;

public class GeneralDto
{
    public GeneralDto(Guid resourceId)
    {
        ResourceId = resourceId;
    }
    public Guid ResourceId { get; set; }
}