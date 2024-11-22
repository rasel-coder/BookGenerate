using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BookGenerate.ViewModels;

public class Book
{
    public int Index { get; set; }
    public string? ISBN { get; set; }
    public string? Title { get; set; }
    public string? Authors { get; set; }
    public string? Publisher { get; set; }
    public string? Language { get; set; }
    public int Like { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? CoverImage { get; set; }
    public List<Review> Reviews { get; set; } = new();
}
