using System.ComponentModel.DataAnnotations;

namespace Taller.Dto.Request
{
    public class GuessCarPriceRequest
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public decimal Price { get; set; }

    }
}
