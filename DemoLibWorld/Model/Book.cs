using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoLibWorld.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Please enter Title, is required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Title name is too short. Please enter valid Title name")]
        public string Title { get; set; }=String.Empty;

        [Required(ErrorMessage = "Please enter Author name, is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Author name is too short. Please enter valid Author name")]
        public string Author { get; set; } = String.Empty;


        [DataType(DataType.Date)]
        [Display(Name="Release Date")]
        [DataValidation(ErrorMessage ="Release date should be less than  current date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage ="Please enter Title rating between 1 and 5")]
        [Range(1,5,ErrorMessage ="Please enter rating only 1-5 ")]
        public string Rating { get; set; }=String.Empty;


        public int BookCategoryId { get; set; }

         
        [Display(Name = "Book Category")]
        public BookCategory? BookCategory  { get; set; } 
    }


    public class DataValidation : ValidationAttribute         //Validation methot that ensure that release date is only be posssible to be in future
    {
        public override bool IsValid(object? value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;

        }


    }
}
