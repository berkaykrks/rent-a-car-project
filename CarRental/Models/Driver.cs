using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarRental.Models
{
    public class Driver
    {
        public int Id { get; set; }
        [Required]
        public string? DriverName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]

        public string? MobileNo { get; set; }
        [Required]
        public int Age{ get; set; }
        [Required]
        public int Experience{ get; set; }
        [Required]        
        public IFormFile DriverImage { get; set; }

		public string? ImagePath { get; set; }
	}
}
