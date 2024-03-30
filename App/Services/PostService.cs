using System.ComponentModel.DataAnnotations;
using App.Data;
using App.Model;
using Microsoft.EntityFrameworkCore;

namespace App.Services;

public class PostService(DataContext context, UserService userService)
{

    public List<Post> Get() => context.Posts.Include(x => x.Comments).Include(x => x.User).ToList();
    public Post? Get(Guid id) => context.Posts.Include(x => x.Comments).Include(x => x.User).FirstOrDefault(x => x.Id == id);

    public Post? Create(Guid userId, Post post)
    {
        bool isValid = Helpers.GenericValidator.TryValidate(post, out ICollection<ValidationResult> errors);
        foreach (ValidationResult result in errors)        
        {
            Console.WriteLine(result.ErrorMessage);
        }
        if (!isValid) throw new Exception("Invalid Post");
        
        Post? doesPostExists = context.Posts.FirstOrDefault(x => x.Title == post.Title);
        User? user = userService.Get(userId);
        if (doesPostExists is not null || user is null) return null;
        
        user.Posts.Add(post);
        context.Posts.Add(post);
        context.SaveChanges();
        return post;
    }
}