namespace Ewone.Data.Entities;

public class Module : Base
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public List<Card> Cards { get; set; } = null!;
}