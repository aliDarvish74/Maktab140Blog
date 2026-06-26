using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Business.Users.Exceptions;
using MaktabBlog.Domain.Users;
using MaktabBlog.ExternalServices.Inquiries;

namespace MaktabBlog.Business.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IInquiryService _inquiryService;
    private readonly INotifierFactory _notifierFactory;

    public UserService(
        IUserRepository userRepository,
        IInquiryService inquiryService,
        INotifierFactory notifierFactory)
    {
        _userRepository = userRepository;
        _inquiryService = inquiryService;
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

    public async Task<Guid> RegisterUserAsync(RegisterUserCommand command)
    {
        var duplicateUser = await _userRepository.GetUserByNationalIdAsync(command.NationalId);

        if (duplicateUser != null)
            throw new DuplicateUserFoundException(command.NationalId);

        if (!await _inquiryService.IsAvailableAsync())
            throw new Exception("Inquiry service is not available.");
        
        var user = new User(command.FirstName, command.LastName, command.NationalId, command.Age);
        await _userRepository.AddAsync(user);
        
        return user.Id;
    }
}