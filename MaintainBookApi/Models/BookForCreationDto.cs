using System.ComponentModel.DataAnnotations;

namespace MaintainBookApi.Models
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a authorName value")]
        [MaxLength(100)]
        public string AuthorName { get; set; } = string.Empty;
    }
}
