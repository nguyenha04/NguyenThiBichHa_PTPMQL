using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Entities
{
public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Range(1, 1000)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    // Navigation
    public Order Order { get; set; }
    public Product Product { get; set; }
}
}