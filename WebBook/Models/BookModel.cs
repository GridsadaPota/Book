using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBook.Models
{
    public class BookModel
    {
        [Key]
        public int Book_Id { get; set; }
        [Required(ErrorMessage="กรุณากรอกข้อมูล")]
        [DisplayName("รหัสหนังสือ")]
        public string Book_Code { get; set; }
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ชื่อหนังสือ")]
        public string Book_Name { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ผู้เขียนหนังสือ")]
        public string Book_Author { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ราคา")]
        [Range(1, 9999)]
        public decimal Price { get; set; } = 0;
        public DateTime Publish_Date { get; set; }
    }
}
