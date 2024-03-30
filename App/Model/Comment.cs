using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model;

public class Comment: Timestamp
{
    public Guid Id { get; set; }
    [Required, MinLength(2)]
    public required string Text { get; set; }

    [ForeignKey(nameof(User))] public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    [ForeignKey(nameof(Post))] public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}