namespace Proks_API.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarNumber { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionTypeId { get; set; }
        public double EngineCapacity { get; set; }
        public int Seats { get; set; }
        public string? ImagePath { get; set; }
        public bool IsAvailable { get; set; }
    }

}
