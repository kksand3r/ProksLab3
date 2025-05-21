namespace Proks_UI.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarNumber { get; set; }
        public int FuelTypeId { get; set; }
        public string? FuelTypeName { get; set; }
        public int TransmissionTypeId { get; set; }
        public string? TransmissionTypeName { get; set; }
        public double EngineCapacity { get; set; }
        public int Seats { get; set; }
    }
}
