using MaktabBlog.Business.Authentications;
using MaktabBlog.WebAPI.Models.Abstractions;
using MaktabBlog.WebAPI.Models.Authentications.RequestDtos;
using MaktabBlog.WebAPI.Models.Authentications.ResponseDtos;
using MaktabBlog.WebAPI.Models.Authentications.ResponseDtos.Dtos;
using MaktabBlog.WebAPI.Models.Users.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace MaktabBlog.WebAPI.Controllers.Authentications;

[ApiController]
[Route("api/auth")]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> AddUserAsync([FromBody] AddUserRequestDto requestDto)
    {
        var result = await _authenticationService.RegisterAsync(requestDto.ToCommand());
        return Ok(new GeneralResponseDto(result.ResourceId));
    }

    [HttpPost("login")]
    public async Task<IActionResult> TokenLoginAsync([FromBody] LoginRequestDto requestDto)
    {
        var result = await _authenticationService.TokenLoginAsync(requestDto.ToCommand());
        return Ok(new TokenLoginResponseDto(TokenDto.FromResult(result)));
    }
}