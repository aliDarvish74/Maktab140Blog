using System.Security.Claims;
using MaktabBlog.Business.Authentications;
using MaktabBlog.Business.Users;
using MaktabBlog.Business.Users.Contracts.Queries;
using MaktabBlog.Business.Users.Contracts.Results.Args;
using MaktabBlog.Domain;
using MaktabBlog.Domain.Users;
using MaktabBlog.WebAPI.Models.Abstractions;
using MaktabBlog.WebAPI.Models.Users.RequestDtos;
using MaktabBlog.WebAPI.Models.Users.ResponseDtos;
using MaktabBlog.WebAPI.Models.Users.ResponseDtos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaktabBlog.WebAPI.Controllers.Users;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
    }
    /// <summary>
    /// This endpoint is going to return a list of users depend on required pagination.
    /// </summary>
    /// <param name="pageNumber">Requested page number</param>
    /// <param name="pageSize">Items in any page</param>
    /// <param name="age">User age limit</param>
    /// <param name="submissionDate">When user created.</param>
    /// <returns>The users</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(List<UserArg>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponseDto<string>),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QueryUsersResponseDto>> GetUsersAsync(
        [FromQuery] int? age = null,
        [FromQuery] DateTime? submissionDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetUsersQuery
        {
            Age = age,
            SubmissionDate = submissionDate,
            Paging = new Paging
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            }
        };
        var result = await _userService.GetUsersAsync(query);
        
        return Ok(new QueryUsersResponseDto(result.Select(UserDto.FromArg).ToList()));
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] Guid userId)
    {
        return Ok();
    }

    /// <summary>
    /// Add user
    /// </summary>
    /// /// <remarks>
    /// Sample Request:
    /// 
    ///     Post /users
    ///     {
    ///         "firstName": "Ali",
    ///         "lastName": "Darvish",
    ///         "NationalId": "1234567890",
    ///         "Password": "!ask2348",
    ///         "age": 31,
    ///     }
    /// </remarks>
    /// <param name="requestDto"></param>
    /// <returns></returns>

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserRequestDto requestDto)
    {
        var user = User;
        
        var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(requesterId, out var id);
        
        await _userService.UpdateUserInfoAsync(requestDto.ToCommand(userId, id));
        return NoContent();
    }

    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> UpdateNationalIdAsync([FromRoute] Guid userId,
        [FromBody] UpdateNationalIdRequestDto requestDto)
    {
        return Ok();
    }
}