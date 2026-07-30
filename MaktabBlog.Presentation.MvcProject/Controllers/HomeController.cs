using System.Diagnostics;
using MaktabBlog.Business.Posts;
using MaktabBlog.Business.Posts.Contracts.Commands;
using MaktabBlog.Business.Posts.Contracts.Queries;
using MaktabBlog.Domain;
using Microsoft.AspNetCore.Mvc;
using MaktabBlog.Presentation.MvcProject.Models;
using MaktabBlog.Presentation.MvcProject.Models.Posts;

namespace MaktabBlog.Presentation.MvcProject.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IPostService postService,
        ILogger<HomeController> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public async Task<ViewResult> Index()
    {
        ViewData["Title"] = "Explore";
        var paging = new Paging();
        var posts = await _postService.GetPostsAsync(new GetPostsQuery(paging));
        return View(posts);
    }

    [HttpGet]
    public ViewResult CreatePost()
    {
        return View();
    }

    [HttpPost]
    public async Task<RedirectToActionResult> CreatePost(CreatePostRequestVm request)
    {
        var command = new AddPostCommand(request.Title, request.Content,
            new Guid("019F4B21-DF5E-7000-8049-F2C503BAC234"));
        
        var result = await _postService.AddPostAsync(command);

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}