using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects;

public class AddItemToBasketDto
{
    [Required]
    public string BasketId { get; set; }

    [Required]
    public string ProductId { get; set; }

    [Required]
    public int Count { get; set; }
}