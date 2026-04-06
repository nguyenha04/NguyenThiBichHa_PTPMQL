
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace FirstWebMVC.Models.Entities
{
    public class Student
    {
        [Key]
        [MinLength(6, ErrorMessage = "Ma sinh vien phai co it nhat 6 ky tu")]
        public string StudentCode { get; set; } = default!;
        [Required(ErrorMessage = "Ho va ten khong duoc de trong")]
        public string FullName { get; set; } = default!;
    }
}