
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace FirstWebMVC.Models.Entities
{
    public class Student
    {
        [Key]
        public string StudentCode { get; set; }
        public string FullName { get; set; }
     
    }
}