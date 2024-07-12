using System.ComponentModel.DataAnnotations;

namespace Talabat2.APIs.Dtos
{
    public class BasketItemDto
    {
        //Will Using This Class To Be Validation What User And FrontEnd Sent To DataBase That What Will They Using
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quentity Must Be One AtLeast")]

        public int Quentity { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }
    }
}
