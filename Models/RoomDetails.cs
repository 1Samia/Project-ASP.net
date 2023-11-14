using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class RoomDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Image1 { get; set; }
        [Required]
        [StringLength(100)]
        public string Image2 { get; set; }
        [Required]
        [StringLength(100)]
        public string Image3 { get; set; }
        [Required]
        [StringLength(300)]
        public string Futures { get; set; }
        [StringLength(200)]
        public string? Food { get; set; }
        [Required]
        public int IdRoom { get; set; }
        [Required]
        public int IdHotel { get; set; }
    }
}
