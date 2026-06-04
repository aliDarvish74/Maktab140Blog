using System.ComponentModel.DataAnnotations;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MaktabBlog.WebAPI;
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository = new UserRepository(new MaktabBlogDbContext());
    
    [HttpGet("getAllUsers")]
    public async Task<ActionResult<List<User>>> GetUsersAsync(
        [FromQuery][Range(1d,10d)] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var skip = (pageNumber - 1) * pageSize;
        var users = await _userRepository.QueryAsync(u => true, pageSize: pageSize, skip: skip);
        return users;
    }
    
    [HttpGet("getUser/{userId:guid}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromQuery] Guid userId, [FromQuery] string? userName)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        Console.WriteLine(userName);
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
    
    
}

public class AddUserRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalId { get; set; }
    public int Age { get; set; }
}