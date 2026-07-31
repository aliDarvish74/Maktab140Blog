using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using MaktabBlog.Business.Abstraction.Contracts.Results;
using MaktabBlog.Business.Abstraction.Exceptions;
using MaktabBlog.Business.Authentications.Constants;
using MaktabBlog.Business.Authentications.Contracts.Commands;
using MaktabBlog.Business.Authentications.Contracts.Results;
using MaktabBlog.Business.Authentications.Exceptions;
using MaktabBlog.Business.Users.Exceptions;
using MaktabBlog.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MaktabBlog.Business.Authentications;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<Role> roleManager,
        IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtSettings = options.Value;
    }
    public async Task<GeneralResult> RegisterAsync(RegisterUserCommand command)
    {
        var duplicateUser = await _userManager.FindByNameAsync(command.NationalId);

        if (duplicateUser != null)
            throw new DuplicateUserFoundException(command.NationalId);
        
        var user = new User(command.FirstName, command.LastName, command.NationalId, command.Age);
        
        var result = await _userManager.CreateAsync(user, command.Password);
        var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.UserRoleName);
        
        if (!result.Succeeded)
        {
            throw new UserRegistrationException(result.Errors.FirstOrDefault()?.Description ??  "Registration failed.");
        }
        return new GeneralResult(user.Id);
    }

    public async Task<TokenLoginResult> TokenLoginAsync(LoginCommand command)
    {
        var result = await _signInManager
            .PasswordSignInAsync(command.Username, command.Password, false, true);

        if(result.IsLockedOut)
            throw new AuthenticationException("User is locked out. Please try again 15 minutes later.");
        
        if(result.IsNotAllowed)
            throw new PermissionDeniedException();

        if (result.RequiresTwoFactor)
        {
            //Todo: at this point you should ass user to enter his/her two factor password.
        }

        if (!result.Succeeded)
            throw new AuthenticationException("Invalid username or password.");
        
        var user = await _userManager.FindByNameAsync(command.Username);
        
        if(user is null)
            throw new UserNotFoundException(command.Username);

        return await GenerateTokenAsync(user);
    }

    public async Task PasswordLoginAsync(LoginCommand command)
    {
        var user = await _userManager.FindByNameAsync(command.Username);
        
        if(user is null)
            throw new AuthenticationException("Username or password is incorrect.");
        
        var result = await _signInManager
            .PasswordSignInAsync(command.Username, command.Password, false, true);

        if(result.IsLockedOut)
            throw new AuthenticationException("User is locked out. Please try again 15 minutes later.");
        
        if(result.IsNotAllowed)
            throw new PermissionDeniedException();

        if (result.RequiresTwoFactor)
        {
            //Todo: at this point you should ass user to enter his/her two factor password.
        }

        if (!result.Succeeded)
            throw new AuthenticationException("Invalid username or password.");
    }

    private async Task<TokenLoginResult> GenerateTokenAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
        };
        
        var userRoles = (await _userManager.GetRolesAsync(user))
            .Select(r => new Claim(ClaimTypes.Role, r)).ToList();

        foreach (var claim in userRoles)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.Name == claim.Value);
            var roleClaims = await _roleManager.GetClaimsAsync(role!);
            claims.AddRange(roleClaims);
        } 
        
        claims.AddRange(userRoles);
        
        var userClaims = await _userManager.GetClaimsAsync(user);

        claims.AddRange(userClaims);

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var expiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);
        
        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expiresIn,
            signingCredentials: credentials);
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token)!;
        var expiresInSeconds = expiresIn.Subtract(DateTime.UtcNow).TotalSeconds;
        return new TokenLoginResult(accessToken, expiresInSeconds);
    }
}