using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoLibWorld.Data;
using DemoLibWorld.Model;

namespace DemoLibWorld.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DemoLibWorld.Data.BookStoreDbContext _context;

        public IndexModel(DemoLibWorld.Data.BookStoreDbContext context)
        {
            _context = context;
        }
        
        //Variables necessary for sorting, for each sort field we must have that variable
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string BookCategorySort { get; set; }
        public string ReleaseDateSort { get; set; }
        public string RatingSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }

       
       
        //It's smart to make Pagination class that can accept any type of model we need,
        //so we don't duplicate code for eery model we want to display


        public PaginationList<Book> Book { get;set; } = default!;
       
        public async Task OnGetAsync(string sortOrder,string searchString,int? pageIndex)
        {
            if (_context.Books != null)
            {
                TitleSort = sortOrder=="title_asc_sort" ? "title_dsc_sort" : "title_asc_sort";
                AuthorSort = sortOrder == "author_asc_sort" ? "author_dsc_sort" : "author_asc_sort";
                BookCategorySort = sortOrder == "bookcategory_asc_sort" ? "bookcategory_dsc_sort" : "bookcategory_asc_sort";
                RatingSort = sortOrder == "rating_asc_sort" ? "rating_dsc_sort" : "rating_asc_sort";
                DateSort = sortOrder == "Date" ? "date_dsc_sort" : "date_asc_sort";

                IQueryable<Book> booksIQ = (from s in _context.Books
                                           select s).Include(y=>y.BookCategory);
               
                if (!String.IsNullOrEmpty(searchString))
                {
                    booksIQ =  booksIQ.Where(y => (y.Title.Contains(searchString) 
                                                    || y.Author.Contains(searchString) || y.BookCategory
                                                    .CategoryName.Contains(searchString))).Include(d => d.BookCategory);
                }

                switch (sortOrder)
                {
                    case "title_asc_sort":
                        booksIQ = booksIQ.OrderBy(s => s.Title);
                        break;

                    case "title_dsc_sort":
                        booksIQ = booksIQ.OrderByDescending(s => s.Title);
                        break;

                    case "author_asc_sort":
                        booksIQ = booksIQ.OrderBy(s => s.Author);
                        break;

                    case "author_dsc_sort":
                        booksIQ = booksIQ.OrderByDescending(s => s.Author);
                        break;

                    case "rating_asc_sort":
                        booksIQ = booksIQ.OrderBy(s => s.Rating);
                        break;

                    case "rating_dsc_sort":
                        booksIQ = booksIQ.OrderByDescending(s => s.Rating);
                        break;

                    default:
                        booksIQ = booksIQ.OrderBy(s => s.Title);
                        break;
                }
                var pageSize = 5;
                Book = await PaginationList<Book>.CreateAsync(booksIQ, pageIndex ?? 1, pageSize);
               

            }
        }
    }
}
