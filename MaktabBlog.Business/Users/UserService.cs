using System.Linq.Expressions;
using MaktabBlog.Business.Abstraction.Exceptions;
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
    public async Task UpdateUserInfoAsync(UpdateUserInfoCommand command)
    {
        var requester = await _userManager.FindByIdAsync(command.RequesterId.ToString());
        
        if (requester == null)
            throw new PermissionDeniedException();
        
        var requesterRoles = await _userManager.GetRolesAsync(requester);

        if (command.Id != requester.Id && !requesterRoles.Contains("Admin"))
            throw new PermissionDeniedException();
        
        var user = requester.Id == command.Id 
            ? requester 
            : await _userManager.FindByIdAsync(command.Id.ToString());

        if (user == null)
            throw new UserNotFoundException(nameof(User));
        
        user.UpdateUserInfo(command.FirstName, command.LastName, user.NationalId, requester.Id, command.Age);
        await _userManager.UpdateAsync(user);

        var notifier = _notifierFactory.GetNotifier();
        
        if (notifier == null)
            throw new ArgumentNullException(nameof(notifier));
        
        notifier.Send("User updated.");
    }

    public async Task<List<UserArg>> GetUsersAsync(GetUsersQuery query)
    {
        Expression<Func<User, bool>> predicate = u => (query.Age == null || u.Age == query.Age) &&
                                                      (query.SubmissionDate == null ||
                                                       query.SubmissionDate.Value.Date == u.CreatedAt.Date);

        Expression<Func<User, UserArg>> projection = u => new UserArg
        {
            Id = u.Id,
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