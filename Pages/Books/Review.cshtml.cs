using e_book_pvt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace e_book_pvt.Pages.Books
{
    [Authorize]
    public class ReviewModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewModel(e_book_pvt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Inject UserManager
        }

        [BindProperty(SupportsGet = true)]
        public int BookId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookName { get; set; }

        [BindProperty(SupportsGet = true)]
        public double BookPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookAuthor { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookImageUrl { get; set; }

        [BindProperty]
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int Rating { get; set; }

        [BindProperty]
        public string Comment { get; set; }

        public List<Review> PreviousReviews { get; set; }
        public IActionResult OnGet(int id)
        {
            var book = _context.Book.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            BookId = book.ID;
            BookName = book.Name;
            BookAuthor = book.Author;
            BookPrice = book.Price;
            BookImageUrl = book.ImageUrl;
            PreviousReviews = _context.Review.Where(r => r.BookID == BookId).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await  _userManager.GetUserAsync(User);
            if (!ModelState.IsValid || currentUser == null)
            {
                return Page();
            }

            var review = new Review
            {
                BookID = BookId,
                Rating = Rating,
                Comment = Comment,
                UserEmail = currentUser.Email,
                UserID = currentUser.Id,
                CreatedAt = DateTime.Now
            };

            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index"); // Redirect to home page or any other page
        }
    }
}
