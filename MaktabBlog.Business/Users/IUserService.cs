using MaktabBlog.Business.Users.Contracts.Commands;
using MaktabBlog.Business.Users.Contracts.Queries;
using MaktabBlog.Business.Users.Contracts.Results;
using MaktabBlog.Business.Users.Contracts.Results.Args;

namespace MaktabBlog.Business.Users;

public interface IUserService
{
    Task UpdateUserInfoAsync(UpdateUserInfoCommand command);
    Task<List<UserArg>> GetUsersAsync(GetUsersQuery query);
    Task GetVipSubscriptionAsync(Guid userId);
    Task<UserArg> GetByIdAsync(string id);
}