namespace EFCodeFirstCreateView.EF;
public class OrderRow
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
    public int Amount { get; set; }
}
