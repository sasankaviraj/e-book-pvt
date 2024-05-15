﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Data;
using e_book_pvt.Models;

namespace e_book_pvt.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;

        public DeleteModel(e_book_pvt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Order.FirstOrDefaultAsync(m => m.ID == id);

            if (orderdetail == null)
            {
                return NotFound();
            }
            else 
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }
            var orderdetail = await _context.Order.FindAsync(id);

            if (orderdetail != null)
            {
                OrderDetail = orderdetail;
                _context.Order.Remove(OrderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
