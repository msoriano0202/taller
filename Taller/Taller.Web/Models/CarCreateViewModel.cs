using Microsoft.Build.Framework;

namespace Taller.Web.Models
{
    public class CarCreateViewModel
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
