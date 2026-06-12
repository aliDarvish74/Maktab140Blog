using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Business.Users.Exceptions;
using MaktabBlog.Domain.Users;
using MaktabBlog.ExternalServices.Notifiers;
using Microsoft.Extensions.DependencyInjection;

namespace MaktabBlog.Business.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly INotifierFactory _notifierFactory;

    public UserService(IUserRepository userRepository, IServiceScopeFactory serviceScopeFactory, INotifierFactory notifierFactory)
    {
        _userRepository = userRepository;
        _notifierFactory = notifierFactory;
    }
    public async Task UpdateUserInfoAsync(UpdateUserInfoCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.Id, true);

        if (user == null)
            throw new UserNotFoundException(nameof(User));
        
        user.UpdateUserInfo(command.FirstName, command.LastName, user.NationalId, command.Age);
        await _userRepository.UpdateAsync(user);

        var notifier = _notifierFactory.GetNotifier();
        
        if (notifier == null)
            throw new ArgumentNullException(nameof(notifier));
        
        notifier.Send("User updated.");
    }
}