namespace Ewone.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string? Name { get; set; }
    public DateTime CreateDate { get; set; }
}