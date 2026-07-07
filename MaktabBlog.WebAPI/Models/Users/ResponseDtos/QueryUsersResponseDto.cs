using MaktabBlog.WebAPI.Models.Abstractions;
using MaktabBlog.WebAPI.Models.Users.ResponseDtos.Dtos;

namespace MaktabBlog.WebAPI.Models.Users.ResponseDtos;

public class QueryUsersResponseDto : BaseResponseDto<List<UserDto>>
{
    public QueryUsersResponseDto(List<UserDto> data) : base(data)
    {
    }
}