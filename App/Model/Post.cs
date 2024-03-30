using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model;

public class Post: Timestamp
{
    public Guid Id { get; set; }
    [Required]
    public required string Title { get; set; }    
    [Required]
    public required string Body { get; set; } 
    
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = [];

}