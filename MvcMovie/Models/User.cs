using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string GoogleId { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
}