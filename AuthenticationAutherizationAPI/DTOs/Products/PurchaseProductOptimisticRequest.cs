using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Products;

public class PurchaseProductOptimisticRequest
{
    [Required]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
