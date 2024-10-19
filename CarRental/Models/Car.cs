using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]

        public string? Brand { get; set; }
        [Required]

        public string? Model { get; set; }
        [Required]

        public int PassingYear { get; set; }
        [Required]

        public string? CarNumber { get; set; }
        [Required]

        public string? Engine { get; set; }
        [Required]

        public string? FuelType { get; set; }
        [Required]

        public int SeatingCapacity { get; set; }
        [Required]

        public IFormFile CarImage { get; set; }

        public string? ImagePath    { get; set; }

    }
}
