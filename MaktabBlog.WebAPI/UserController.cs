using System.ComponentModel.DataAnnotations;
using MaktabBlog.Business.Users;
using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Users;
using Microsoft.AspNetCore.Mvc;

namespace MaktabBlog.WebAPI;
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
    
    [HttpGet()]
    public async Task<ActionResult<List<User>>> GetUsersAsync(
        [FromQuery][Range(1d,10d)] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var skip = (pageNumber - 1) * pageSize;
        var users = await _userRepository.QueryAsync(u => true, pageSize: pageSize, skip: skip);
        return users;
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if(user == null)
            return NotFound("User not found!");
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] AddUserRequestDto requestDto)
    {
        try
        {
            var user = new User(requestDto.FirstName, requestDto.LastName, requestDto.NationalId, requestDto.Age);
            await _userRepository.AddAsync(user);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
        
        return Created();
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

public class AddUserRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalId { get; set; }
    public int Age { get; set; }
}

public class UpdateUserRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public UpdateUserInfoCommand ToCommand(Guid userId)
    {
        return new UpdateUserInfoCommand
        {
            Id = userId,
            FirstName = FirstName,
            LastName = LastName,
            Age = Age
        };
    }
}

public class UpdateNationalIdRequestDto
{
    public string NationalId { get; set; }
}