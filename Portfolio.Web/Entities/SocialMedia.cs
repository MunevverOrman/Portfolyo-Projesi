using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        [Required(ErrorMessage="Bu alan boş bırakılamaz")]
        public string PlatformName { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string ProfileUrl { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string PlatformIcon { get; set; }
    }
}
