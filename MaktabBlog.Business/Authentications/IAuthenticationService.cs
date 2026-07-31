using MaktabBlog.Business.Abstraction.Contracts.Results;
using MaktabBlog.Business.Authentications.Contracts.Commands;
using MaktabBlog.Business.Authentications.Contracts.Results;
using MaktabBlog.Business.Users.Contracts.Commands;

namespace MaktabBlog.Business.Authentications;

public interface IAuthenticationService
{
    Task<GeneralResult> RegisterAsync(RegisterUserCommand command);
    Task<TokenLoginResult> TokenLoginAsync(LoginCommand command);
    Task PasswordLoginAsync(LoginCommand command);
}