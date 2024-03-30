using App.Data;
using App.Helpers;
using App.Model;
using App.Services;

DataContext context = new();
UserService userService = new(context);
PostService postService = new(context, userService);
CommentService commentService = new(context, postService);

// FakeData.FillDb(userService, postService, commentService); // Add Random Data to DB

User u2 = userService.Get().First();
Comment c1 = u2.Comments.First();
Post post = c1.Post;
Printer.PrintUser(c1.User); // User
Printer.PrintPost(post); // Post
Printer.PrintComment(c1); // Comment