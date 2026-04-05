using System.ComponentModel.DataAnnotations;

namespace CetStudentBook.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [StringLength(200)]
        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
