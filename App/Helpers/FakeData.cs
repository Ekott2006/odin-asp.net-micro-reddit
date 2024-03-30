using System.Text.Json;
using App.Data;
using App.Model;
using App.Services;
using Bogus;

namespace App.Helpers;

public static class FakeData
{
    public static void FillDb(UserService userService, PostService postService, CommentService commentService)
    {
        Random random = new();
        List<User> users = new UserFaker().Generate(3);
        users.ForEach(user =>
        {
            userService.Create(user);

            List<Post> posts = new PostFaker().Generate(random.Next(2, 5));
            foreach (Post post in posts)
            {
                postService.Create(user.Id, post);

                List<Comment> comments = new CommentFaker().Generate(random.Next(1, 10));
                comments.ForEach(comment => commentService.Create(user.Id, post.Id, comment));
            }
        });
    }

}

public sealed class UserFaker : Faker<User>
{
    public UserFaker()
    {
        RuleFor(e => e.Email, f => f.Internet.Email());
        RuleFor(e => e.Password, f => f.Internet.Password());
        RuleFor(e => e.Username, f => f.Internet.UserName());
        RuleFor(e => e.Id, _ => Guid.NewGuid());
    }
}

public sealed class PostFaker : Faker<Post>
{
    public PostFaker()
    {
        RuleFor(e => e.Body, f => f.Lorem.Paragraphs(3, "\n"));
        RuleFor(e => e.Title, f => f.Lorem.Sentence());
        RuleFor(e => e.Id, _ => Guid.NewGuid());
    }
}

public sealed class CommentFaker : Faker<Comment>
{
    public CommentFaker()
    {
        RuleFor(e => e.Text, f => f.Lorem.Sentences(3, "."));
        RuleFor(e => e.Id, _ => Guid.NewGuid());
    }
}