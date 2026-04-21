using System.ComponentModel.DataAnnotations;

namespace FirstWebMVC.Models.Entities
{
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(150)]
    public string ProductName { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
}
}