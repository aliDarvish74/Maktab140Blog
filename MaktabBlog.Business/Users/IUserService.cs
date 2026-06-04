using MaktabBlog.Business.Users.Contracts.Commands;

namespace MaktabBlog.Business.Users;

public interface IUserService
{
    Task UpdateUserInfoAsync(UpdateUserInfoCommand command);
}