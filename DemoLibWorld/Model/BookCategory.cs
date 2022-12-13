using System.ComponentModel.DataAnnotations;

namespace DemoLibWorld.Model
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }=String.Empty;
        public List<Book> Books { get; set; }=new List<Book>();
    }
}
