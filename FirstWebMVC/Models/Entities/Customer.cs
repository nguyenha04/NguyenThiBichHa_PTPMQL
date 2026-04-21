using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{


public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Tên khách hàng không được để trống")]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Phone { get; set; }

    // 1 khách hàng có nhiều đơn hàng
    public ICollection<Order> Orders { get; set; }
}
}