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
    public class IndexModel : PageModel
    {
        private readonly DemoLibWorld.Data.BookStoreDbContext _context;

        public IndexModel(DemoLibWorld.Data.BookStoreDbContext context)
        {
            _context = context;
        }

        public IList<BookCategory> BookCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BookCategory != null)
            {
                BookCategory = await _context.BookCategory.ToListAsync();
            }
        }
    }
}
