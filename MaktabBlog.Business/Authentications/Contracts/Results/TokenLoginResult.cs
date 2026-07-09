namespace MaktabBlog.Business.Authentications.Contracts.Results;

public record TokenLoginResult(string AccessToken, double ExpiresIn);