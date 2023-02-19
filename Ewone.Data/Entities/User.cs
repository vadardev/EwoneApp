namespace Ewone.Data.Entities;

public class User : Base
{
    public string Email { get; set; } = null!;
    public string? Name { get; set; }

    public List<Module> Modules { get; set; } = null!;
    public List<Card> Authors { get; set; } = null!;
}