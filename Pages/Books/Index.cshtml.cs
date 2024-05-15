using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Data;
using e_book_pvt.Models;
using Microsoft.AspNetCore.Authorization;

namespace e_book_pvt.Pages.Books
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;

        public IndexModel(e_book_pvt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Book != null)
            {
                Book = await _context.Book.ToListAsync();
            }
        }
    }
}
