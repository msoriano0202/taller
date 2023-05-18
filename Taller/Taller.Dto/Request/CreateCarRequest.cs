using System.ComponentModel.DataAnnotations;

namespace Taller.Dto.Request
{
    public class CreateCarRequest
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Doors { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
