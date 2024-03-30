using App.Model;

namespace App.Helpers;

public static class Printer
{
    public static void PrintPost(Post post)
    {
        Console.WriteLine("**Post Details**");
        Console.WriteLine($"User UserName: {post.User.Username}"); 
        Console.WriteLine($"ID: {post.Id}");
        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine($"User ID: {post.UserId}");
        
        // Handle Comments if needed (might require looping)
        if (post.Comments.Count == 0) return;
        Console.WriteLine("Comments:");
        foreach (Comment c in post.Comments)
        {
            Console.WriteLine($"- {c.Text}"); // Assuming Content property in Comment
        }

        Console.WriteLine();
    }

    public static void PrintUser(User user)
    {
        Console.WriteLine("**User Details**");
        Console.WriteLine($"ID: {user.Id}");
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine($"Password: {user.Password}");
        Console.WriteLine($"Username: {user.Username}");

        // Handle Posts if needed (might require looping)
        if (user.Posts.Count == 0) return;
        Console.WriteLine("Posts:");
        foreach (Post c in user.Posts)
        {
            Console.WriteLine($"- {c.Title}");
        }

        // Handle Comments if needed (might require looping)
        if (user.Comments.Count == 0) return;
        Console.WriteLine("Comments:");
        foreach (Comment c in user.Comments)
        {
            Console.WriteLine($"- {c.Text}");
        }
        
        Console.WriteLine();
    }
    
    
    public static void PrintComment(Comment comment)
    {
        Console.WriteLine("**Comment Details**");
        Console.WriteLine($"User UserName: {comment.User.Username}"); // Assuming Username property in User
        Console.WriteLine($"Post Title: {comment.Post.Title}"); // Assuming Username property in User
        
        Console.WriteLine($"ID: {comment.Id}");
        Console.WriteLine($"Text: {comment.Text}");
        Console.WriteLine();
    }
}