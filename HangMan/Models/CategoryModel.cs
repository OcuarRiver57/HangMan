using System.ComponentModel.DataAnnotations;

namespace HangMan.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category is Required")]
        [StringLength(50)]
        public string Name { get; set; } = "Unknown";
        [StringLength(150)]
        public string Description { get; set; } = "";
    }
}
