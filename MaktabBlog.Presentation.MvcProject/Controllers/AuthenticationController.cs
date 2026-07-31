using MaktabBlog.Business.Authentications;
using MaktabBlog.Business.Users;
using MaktabBlog.Domain.Users;
using MaktabBlog.Presentation.MvcProject.Models.Authentications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaktabBlog.Presentation.MvcProject.Controllers;

public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IAuthenticationService authenticationService,
        SignInManager<User> signInManager,
        ILogger<AuthenticationController>  logger)
    {
        _authenticationService = authenticationService;
        _signInManager = signInManager;
        _logger = logger;
    }

    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserVm model)
    {
        var command = model.ToCommand();
        var registrationResult = await _authenticationService.RegisterAsync(command);
        
        return RedirectToAction(nameof(Login));
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm model, [FromQuery] string returnUrl)
    {
        var command = model.ToCommand();
        
        try
        {
            await _authenticationService.PasswordLoginAsync(command);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e,"Login failed. Username: {UserName}, Password: {Password}",
                model.NationalId, model.Password);
            
            TempData["Error"] = e.Message;
            TempData["HasError"] = true;
            ViewData["Test"] = "Testing ViewData life time. in exception.";
            ViewBag.Test = "Testing ViewBag life time. in exception.";
            return RedirectToAction(nameof(Login));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}