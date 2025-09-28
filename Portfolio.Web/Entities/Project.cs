using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        [MinLength(5,ErrorMessage ="Proje Adı en az 5 karakterden olmalıdır.")]
        [MaxLength(50, ErrorMessage = "Proje Adı en fazla 50 karakter olmalıdır.")]
        [Required(ErrorMessage ="proje adı boş bırakılamaz")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "proje adı boş bırakılamaz")]
        public string Description { get; set; }
        [Required(ErrorMessage = "proje adı boş bırakılamaz")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Github boş bırakılamaz")]
        public string GithubUrl { get; set; }
        [Required(ErrorMessage = "Kategori boş bırakılamaz")]
     
        public int CategoryId { get; set; }
        //navigation property
        public Category? Category { get; set; }


    }
}
