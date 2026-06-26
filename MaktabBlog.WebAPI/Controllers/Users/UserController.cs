using System.ComponentModel.DataAnnotations;
using MaktabBlog.Business.Users;
using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Domain.Users;
using MaktabBlog.Domain.Users.ViewModels;
using MaktabBlog.WebAPI.Models.Abstractions;
using MaktabBlog.WebAPI.Models.Users.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace MaktabBlog.WebAPI.Controllers.Users;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public UserController(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }
    /// <summary>
    /// This endpoint is going to return a list of users depend on required pagination.
    /// </summary>
    /// <param name="pageNumber">Requested page number</param>
    /// <param name="pageSize">Items in any page</param>
    /// <returns>The users</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponseDto<string>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsersAsync(
        [FromQuery] [Range(1d, 10d)] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var skip = (pageNumber - 1) * pageSize;
        
        var users = await _userRepository
            .GetUsersAsViewModelAsync(u => new DetailedUserViewModel()
            {
                CreatedAt = u.CreatedAt,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age
            });
        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if(user == null)
            return NotFound("User not found!");
        
        return Ok(user);
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
    ///         "age": 31,
    ///     }
    /// </remarks>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] AddUserRequestDto requestDto)
    {
        var command = new RegisterUserCommand(requestDto.FirstName,  requestDto.LastName, requestDto.NationalId,  requestDto.Age);
        var userId = await _userService.RegisterUserAsync(command);
        
        return Created("/users", userId);
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserRequestDto requestDto)
    {
        await _userService.UpdateUserInfoAsync(requestDto.ToCommand(userId));
        return NoContent();
    }

    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> UpdateNationalIdAsync([FromRoute] Guid userId,
        [FromBody] UpdateNationalIdRequestDto requestDto)
    {
        var user = await _userRepository.GetByIdAsync(userId, true);
        if(user == null)
            return NotFound("User not found!");
        
        user.UpdateUserInfo(user.FirstName,user.LastName,requestDto.NationalId);
        await _userRepository.UpdateAsync(user);
        
        return NoContent();
    }
}