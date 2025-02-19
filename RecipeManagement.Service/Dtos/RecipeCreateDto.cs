using System.ComponentModel.DataAnnotations;

namespace RecipeManagement.Service.Dtos
{
    public class RecipeCreateDto
{
    [Required]
    public string Title { get; set; }="";

    [MaxLength(1000)]
    public string Description { get; set; }="";

    [MaxLength(2000)]
    public string Ingredients { get; set; }="";

    [Required]
    public int CookingTime { get; set; }

    [MaxLength(2000)]
    public string Instructions { get; set; }="";

    [MaxLength(500)]
    public string ImageUrl { get; set; }="";

    [Required]
    public int StatusId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int UserId { get; set; }
}

}