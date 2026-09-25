namespace AuthenticationAutherizationAPI.DTOs.Products;

public class ConcurrencyTestResponse
{
    public string Strategy { get; set; } = string.Empty;

    public int TotalRequests { get; set; }

    public int SuccessfulPurchases { get; set; }

    public int ConcurrencyConflicts { get; set; }

    public int OutOfStockCount { get; set; }

    public int InitialStock { get; set; }

    public int FinalStock { get; set; }

    public long ElapsedMilliseconds { get; set; }

    public List<string> Logs { get; set; } = new();
}
