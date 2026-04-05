using System.ComponentModel.DataAnnotations;

namespace CetStudentBook.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
}
