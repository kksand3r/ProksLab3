using System.ComponentModel.DataAnnotations;

namespace Proks_API.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}
