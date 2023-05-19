using System.ComponentModel.DataAnnotations;

namespace Taller.Web.Models
{
    public class CarUpdateViewModel : CarCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
