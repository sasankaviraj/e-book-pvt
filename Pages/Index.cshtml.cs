﻿using e_book_pvt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace e_book_pvt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly e_book_pvt.Data.ApplicationDbContext _context;
        private readonly HttpClient _httpClient;  // Inject HttpClient
        private readonly UserManager<IdentityUser> _userManager;  // Inject UserManager
        public IList<Book> Book { get; set; } = default!;
        public IList<Cart> CartItems { get; set; }
        public IndexModel(ILogger<IndexModel> logger, e_book_pvt.Data.ApplicationDbContext context, IHttpClientFactory httpClientFactory, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _httpClient = httpClientFactory.CreateClient();  // Create HttpClient instance
            _userManager = userManager;  // Initialize UserManager
        }

        public async Task OnGetAsync()
        {
            Book = new List<Book>();

            if (_context.Book != null)
            {
                Book = await _context.Book.ToListAsync();

                // Iterate through each book to fetch and calculate the average rating
                foreach (var book in Book)
                {
                    // Fetch reviews related to the current book
                    var reviews = await _context.Review.Where(r => r.BookID == book.ID).ToListAsync();

                    // Calculate the average rating
                    if (reviews.Any())
                    {
                        // Calculate the average rating based on all reviews
                        double averageRating = reviews.Select(r => r.Rating).Average();
                        // Round the average rating to the nearest integer
                        book.Rating = (int)Math.Round(averageRating);
                    }
                    else
                    {
                        // If no reviews found, set the rating to 0
                        book.Rating = 0;
                    }
                }
            }
        }
        
        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to login page or handle unauthorized access as per your requirement
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var user = await _userManager.GetUserAsync(User);
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var cartItem = new Cart
            {
                BookID = book.ID,
                Name = book.Name,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                UserID = user.Id
            };

            _context.Cart.Add(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}