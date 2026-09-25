namespace AuthenticationAutherizationAPI.DTOs.Products;

public class PurchaseProductResponse
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int RemainingStock { get; set; }

    public bool Purchased { get; set; }

    public string Message { get; set; } = string.Empty;

    public string LockStrategy { get; set; } = string.Empty;

    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}