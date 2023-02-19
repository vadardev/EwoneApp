namespace Ewone.Data.Entities;

public class Card : Base
{
    public int WordId { get; set; }
    public Word Word { get; set; } = null!;

    public string Definition { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public List<string> Examples { get; set; } = null!;

    public int? ParentId { get; set; }
    public Card? Parent { get; set; }

    public int? AuthorId { get; set; }
    public User? Author { get; set; }
    
    public int? ModuleId { get; set; }
    public Module? Module { get; set; }
}