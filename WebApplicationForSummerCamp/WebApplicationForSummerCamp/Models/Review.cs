using System.ComponentModel.DataAnnotations;

namespace WebApplicationForSummerCamp.Models
{
    public class Review
    {
        //public int ReviewId { get; set; }

        public int? Rating { get; set; }

        public string Comment { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "AnouncementId is Required")]
        public int AnnouncementId { get; set; }

        
    }
}