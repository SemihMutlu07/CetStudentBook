namespace CetStudentBook.Models
{
    public class Book
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = "";

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(200, MinimumLength = 2)]
        public string Author { get; set; } = "";

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public System.DateTime PublishDate { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Range(1, 10000)]
        public int PageCount { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public bool IsSecondHand { get; set; }
    }
}