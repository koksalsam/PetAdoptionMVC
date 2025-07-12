using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PetAdoptionMVC.Models
{
    public class PetAd
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        [Required]
        public string Gender { get; set; } = string.Empty;

        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}