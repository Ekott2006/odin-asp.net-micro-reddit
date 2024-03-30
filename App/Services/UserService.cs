using System.ComponentModel.DataAnnotations;
using App.Data;
using App.Model;
using Microsoft.EntityFrameworkCore;

namespace App.Services;

public class UserService(DataContext context)
{
    public ICollection<User> Get() => context.Users.Include(x => x.Posts).Include(x => x.Comments).ToList();
    public User? Get(Guid id) => context.Users.Include(x => x.Posts).Include(x => x.Comments).FirstOrDefault(x => x.Id == id);

    public User? Create(User user)
    {
        bool isValid = Helpers.GenericValidator.TryValidate(user, out ICollection<ValidationResult> errors);
        foreach (ValidationResult result in errors)        
        {
            Console.WriteLine(result.ErrorMessage);
        }
        if (!isValid) throw new Exception("Invalid User");
        
        User? doesUserExists = context.Users.FirstOrDefault(x => x.Email == user.Email || x.Username == user.Username);
        if (doesUserExists is not null) return null;
        user.Email = user.Email.ToLower();
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }
}