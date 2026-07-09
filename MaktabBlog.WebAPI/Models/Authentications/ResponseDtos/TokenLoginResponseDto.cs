using MaktabBlog.WebAPI.Models.Abstractions;
using MaktabBlog.WebAPI.Models.Authentications.ResponseDtos.Dtos;

namespace MaktabBlog.WebAPI.Models.Authentications.ResponseDtos;

public class TokenLoginResponseDto : BaseResponseDto<TokenDto>
{
    public TokenLoginResponseDto(TokenDto data) : base(data)
    {
    }
}