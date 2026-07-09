using MaktabBlog.Business.Authentications.Contracts.Results;

namespace MaktabBlog.WebAPI.Models.Authentications.ResponseDtos.Dtos;

public class TokenDto
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }

    public static TokenDto FromResult(TokenLoginResult result) => new TokenDto
    {
        AccessToken = result.AccessToken,
        ExpiresIn = result.ExpiresIn,
    };
}