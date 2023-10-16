using System.ComponentModel.DataAnnotations;

namespace Dogshouseservice.API.Models.DTO
{
    public class AddDogRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Color has to be a maximum of 100 characters")]
        public string Color { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "Tail length has to be in the range of 0 to 200")]
        public int Tail_length { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Weight has to be in the range of 0 to 100")]
        public int Weight { get; set; }
    }
}