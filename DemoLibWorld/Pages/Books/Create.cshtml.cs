using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoLibWorld.Data;
using DemoLibWorld.Model;

namespace DemoLibWorld.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly DemoLibWorld.Data.BookStoreDbContext _context;

        public CreateModel(DemoLibWorld.Data.BookStoreDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Options { get; set; }
        public IActionResult OnGet()
        {
            Options = _context.BookCategory.Select(a =>
                                   new SelectListItem
                                   {
                                       Value = a.Id.ToString(),
                                       Text = a.CategoryName
                                   }) .ToList();

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }
       
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {

                await  _context.Books.AddAsync(Book);
                await _context.SaveChangesAsync();
                
            }
        
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            //Fix Release date input field, if its in past won't create record
            //should be restricted only to present and future!
        }
    }
}
