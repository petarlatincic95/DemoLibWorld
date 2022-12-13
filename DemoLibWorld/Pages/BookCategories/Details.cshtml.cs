using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoLibWorld.Data;
using DemoLibWorld.Model;

namespace DemoLibWorld.Pages.BookCategories
{
    public class DetailsModel : PageModel
    {
        private readonly DemoLibWorld.Data.BookStoreDbContext _context;

        public DetailsModel(DemoLibWorld.Data.BookStoreDbContext context)
        {
            _context = context;
        }

      public BookCategory BookCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BookCategory == null)
            {
                return NotFound();
            }

            var bookcategory = await _context.BookCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (bookcategory == null)
            {
                return NotFound();
            }
            else 
            {
                BookCategory = bookcategory;
            }
            return Page();
        }
    }
}
