using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoLibWorld.Data;
using DemoLibWorld.Model;

namespace DemoLibWorld.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly DemoLibWorld.Data.BookStoreDbContext _context;

        public EditModel(DemoLibWorld.Data.BookStoreDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public List<SelectListItem> Options { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Where(y => y.Id == id).Include(x=>x.BookCategory).FirstOrDefaultAsync();

          

            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            Options = _context.BookCategory.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.CategoryName
                                  }).ToList();

            return Page();
            //ViewData["BookCategoryId"] = new SelectList(_context.BookCategory, "Id", "Id");
          
          
         
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // _context.Attach(Book).State = EntityState.Modified;
             _context.Books.Update(Book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
          return _context.Books.Any(e => e.Id == id);
        }
    }
}
