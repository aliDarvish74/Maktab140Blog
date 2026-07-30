using System.ComponentModel.DataAnnotations;

namespace MaktabBlog.Presentation.MvcProject.Models.Posts;

public class CreatePostRequestVm
{
    [Required(ErrorMessage = "Title is required", AllowEmptyStrings =  false)]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
    public string Title { get; set; }   
    
    [Required(ErrorMessage = "Content is required",  AllowEmptyStrings = false)]
    [MinLength(10, ErrorMessage = "Content must be at least 10 characters long")]
    public string Content { get; set; }
};