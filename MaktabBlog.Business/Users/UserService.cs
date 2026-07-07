using System.Linq.Expressions;
using MaktabBlog.Business.Notifiers;
using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Business.Users.Contracts.Queries;
using MaktabBlog.Business.Users.Contracts.Results;
using MaktabBlog.Business.Users.Contracts.Results.Args;
using MaktabBlog.Business.Users.Exceptions;
using MaktabBlog.Domain.Users;
using MaktabBlog.ExternalServices.Inquiries;
using Microsoft.AspNetCore.Identity;

namespace MaktabBlog.Business.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IInquiryService _inquiryService;
    private readonly INotifierFactory _notifierFactory;

    public UserService(
        IUserRepository userRepository,
        UserManager<User> userManager,
        IInquiryService inquiryService,
        INotifierFactory notifierFactory)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _inquiryService = inquiryService;
        _notifierFactory = notifierFactory;
    }
    /*public async Task UpdateUserInfoAsync(UpdateUserInfoCommand command)
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
    }*/

    public async Task<string> RegisterUserAsync(RegisterUserCommand command)
    {
        var duplicateUser = await _userManager.FindByNameAsync(command.NationalId);

        if (duplicateUser != null)
            throw new DuplicateUserFoundException(command.NationalId);

        if (!await _inquiryService.IsAvailableAsync())
            throw new Exception("Inquiry service is not available.");
        
        var user = new User(command.FirstName, command.LastName, command.NationalId, command.Age);
        
        var result = await _userManager.CreateAsync(user, command.Password);
        
        if (!result.Succeeded)
        {
            throw new UserRegistrationException(result.Errors.FirstOrDefault()?.Description ??  "Registration failed.");
        }
        return user.Id;
    }

    public async Task<List<UserArg>> GetUsersAsync(GetUsersQuery query)
    {
        Expression<Func<User, bool>> predicate = u => (query.Age == null || u.Age == query.Age) &&
                                                      (query.SubmissionDate == null || query.SubmissionDate.Value.Date == u.CreatedAt.Date);

        Expression<Func<User, UserArg>> projection = u => new UserArg
        {
            Id = Guid.Parse(u.Id),
            FirstName = u.FirstName,
            LastName = u.LastName,
            Username = u.UserName,
            NationalId = u.NationalId,
            Email = u.Email,
            Age = u.Age
        };

        return await _userRepository.QueryUsersAsync(predicate, query.Paging, projection);
    }
}