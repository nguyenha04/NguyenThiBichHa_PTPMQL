using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{
    public class Faculty
    {
        [Key]
        [Required(ErrorMessage = "Ma khoa khong duoc de trong")]
        public string FacultyId { get; set; } = default!;
        [Required(ErrorMessage = "Ten khoa khong duoc de trong")]
        public string FacultyName { get; set; } = default!;
        public virtual ICollection<Student>? Students { get; set; }
    }
}