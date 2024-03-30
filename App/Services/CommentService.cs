using System.ComponentModel.DataAnnotations;
using App.Data;
using App.Helpers;
using App.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Services;

public class CommentService(DataContext context, PostService postService)
{
    private IIncludableQueryable<Comment, Post> GetComments => context.Comments.Include(x => x.User).Include(x => x.Post);
    
    public List<Comment> Get() => GetComments.ToList();
    public Comment? Get(Guid id) =>
        GetComments.FirstOrDefault(x => x.Id == id);

    public Comment? Create(Guid userId, Guid postId, Comment comment)
    {
        bool isValid = GenericValidator.TryValidate(comment, out ICollection<ValidationResult> errors);
        foreach (ValidationResult result in errors)
        {
            Console.WriteLine(result.ErrorMessage);
        }

        if (!isValid) throw new Exception("Invalid Comment");

        Post? post = postService.Get(postId);
        if (post is null) return null;

        comment.PostId = postId;
        comment.UserId = userId;
        context.Comments.Add(comment);
        context.SaveChanges();
        return comment;
    }
}