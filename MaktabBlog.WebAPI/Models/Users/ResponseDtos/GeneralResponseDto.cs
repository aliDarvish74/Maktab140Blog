using MaktabBlog.WebAPI.Models.Abstractions;
using MaktabBlog.WebAPI.Models.Users.ResponseDtos.Dtos;

namespace MaktabBlog.WebAPI.Models.Users.ResponseDtos;

public class GeneralResponseDto : BaseResponseDto<GeneralDto>
{
    public GeneralResponseDto(Guid resourceId) : base(new GeneralDto(resourceId))
    {
    }

    /// <inheritdoc/>
    public GeneralResponseDto(string message, string code) : base(message, code)
    {
    }
}