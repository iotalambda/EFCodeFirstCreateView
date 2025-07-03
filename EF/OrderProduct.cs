namespace EFCodeFirstCreateView.EF;
public class OrderProduct
{
    public string? OrderNumber { get; set; }
    public string? EAN { get; set; }
    public int Amount { get; set; }
    public decimal TotalPriceEur { get; set; }
}
