using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Domain.Users;

namespace MaktabBlog.Business.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task UpdateUserInfoAsync(UpdateUserInfoCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.Id, true);

        if (user == null)
            throw new KeyNotFoundException();
        
        user.UpdateUserInfo(command.FirstName, command.LastName, user.NationalId, command.Age);
        await _userRepository.UpdateAsync(user);
    }
}