using System.ComponentModel.DataAnnotations;

namespace Proks_UI.DTOs
{
    public class TransmissionTypeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }
    }
}
