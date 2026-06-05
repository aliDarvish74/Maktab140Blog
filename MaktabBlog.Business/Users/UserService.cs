using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MaktabBlog.Business.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserService(IUserRepository userRepository, IServiceScopeFactory serviceScopeFactory)
    {
        _userRepository = userRepository;
        _serviceScopeFactory = serviceScopeFactory;
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