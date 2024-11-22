using Bogus;
using BookGenerate.ViewModels;

namespace BookGenerate.Services;

public class BookGeneratorService
{
    public List<Book> GenerateBooks(BookPage model, string region = "en-US", int count = 20)
    {
        string validRegion = region switch
        {
            "en-US" => "en",
            "de-DE" => "de",
            "fr-FR" => "fr",
            _ => "en"
        };

        var faker = new Faker<Book>(validRegion)
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Authors, f => string.Join(", ", f.Name.FullName(), f.Name.FullName()))
            .RuleFor(b => b.ISBN, f => f.Commerce.Ean13())
            .RuleFor(b => b.Publisher, f => f.Company.CompanyName())
            .RuleFor(b => b.CoverImage, f => $"https://picsum.photos/seed/{f.Random.Number(0, 100)}/100/150")
            .RuleFor(b => b.Language, f => validRegion switch
            {
                "en" => "English",
                "de" => "German",
                "fr" => "French",
                _ => "Unknown"
            })
            .RuleFor(b => b.Like, f => f.Random.Number(0, model.Like))
            .RuleFor(b => b.CreationDate, f => f.Date.Past(10))
            .RuleFor(b => b.Reviews, f =>
            {
                int reviewCount = f.Random.Number(1, (int)model.Review);
                var reviews = new Faker<Review>(validRegion)
                    .RuleFor(r => r.Reviewer, f => f.Name.FullName())
                    .RuleFor(r => r.Content, f => f.Lorem.Paragraph())
                    .Generate(reviewCount);

                return reviews;
            });

        var books = new List<Book>();
        for (int i = 0; i < count; i++)
        {
            var book = faker.Generate();
            book.Index = model.Page * count + i + 1;
            books.Add(book);
        }

        return books;
    }



    private int RandomizedCount(double average) =>
        (int)Math.Floor(average) + (new Random().NextDouble() < (average % 1) ? 1 : 0);
}
