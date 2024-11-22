using Bogus;
using BookGenerate.Models;
using BookGenerate.Services;
using BookGenerate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace BookGenerate.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookGeneratorService bookGeneratorService;
        private readonly ILogger<HomeController> logger;

        public List<Book> Books { get; set; } = new();
        public string Region { get; set; } = "en-US";
        public int Seed { get; set; } = 58933423;
        public double AvgLikes { get; set; } = 3.5;
        public double AvgReviews { get; set; } = 2.5;

        public HomeController(BookGeneratorService bookGeneratorService
            , ILogger<HomeController> logger)
        {
            this.bookGeneratorService = bookGeneratorService;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index(BookPage model)
        {
            Random random = new Random();
            model.Page = 0;
            BookPage bookPage = new BookPage();
            bookPage.Books = bookGeneratorService.GenerateBooks(model, model?.Region);
            bookPage.Region = model.Region;
            bookPage.Seed = random.Next(999999, 99999999);
            bookPage.Like = model.Like;
            bookPage.Review = model.Review;
            bookPage.Page = model.Page + 2;
            return View(bookPage);
        }

        [HttpGet]
        public IActionResult GetMoreBooks(BookPage model)
        {
            List<Book> books = bookGeneratorService.GenerateBooks(model, model?.Region, 10);
            return PartialView("_BookRows", books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
