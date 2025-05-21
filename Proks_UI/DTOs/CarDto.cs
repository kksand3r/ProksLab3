using System.ComponentModel.DataAnnotations;

namespace Proks_UI.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "BrandId is required")]
        public int BrandId { get; set; }

        public string? BrandName { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "CarNumber is required")]
        [StringLength(20, ErrorMessage = "CarNumber length can't be more than 20 characters.")]
        public string CarNumber { get; set; }

        [Required(ErrorMessage = "FuelTypeId is required")]
        public int FuelTypeId { get; set; }

        public string? FuelTypeName { get; set; }

        [Required(ErrorMessage = "TransmissionTypeId is required")]
        public int TransmissionTypeId { get; set; }

        public string? TransmissionTypeName { get; set; }

        [Range(0.1, 20.0, ErrorMessage = "EngineCapacity must be between 0.1 and 20.0")]
        public double EngineCapacity { get; set; }

        [Range(1, 20, ErrorMessage = "Seats must be between 1 and 20")]
        public int Seats { get; set; }
    }
}
