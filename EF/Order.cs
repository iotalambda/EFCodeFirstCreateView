namespace EFCodeFirstCreateView.EF;
public class Order
{
    public int Id { get; set; }
    public string? OrderNumber { get; set; }
    public List<OrderRow> OrderRows { get; set; } = [];
}
