using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BookGenerate.ViewModels;

public class BookPage
{
    public BookPage()
    {
        Books = new List<Book>();
    }
    public int Page { get; set; }
    public string? Region { get; set; }
    public int Seed { get; set; }
    public int Like { get; set; } = 100;
    public double Review { get; set; } = 4.7;
    public List<Book> Books { get; set; } = new();
}
