using System.ComponentModel.DataAnnotations;

namespace WebBook.Models
{
    public class BookViewModel
    {
        [Key]
        public int Book_Id { get; set; }
        public string Book_Code { get; set; }
        public string Book_Name { get; set; }
        public string Book_Author { get; set; }
        public decimal Price { get; set; }
        public DateTime Publish_Date { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Modify_Date { get; set; }
    }
}
