using EFCodeFirstCreateView.EF;
using Microsoft.EntityFrameworkCore;

using var ctx = new MyDbContext();

ctx.Database.EnsureDeleted();
ctx.Database.EnsureCreated();

var orderProductsQuerySql =
    (from o in ctx.Orders
     join or in ctx.OrderRows on o.Id equals or.OrderId
     join p in ctx.Products on or.ProductId equals p.Id
     select new OrderProduct
     {
         OrderNumber = o.OrderNumber,
         EAN = or.Product!.EAN,
         Amount = or.Amount,
         TotalPriceEur = or.Amount * or.Product.PriceEur
     }).ToQueryString();
ctx.Database.ExecuteSqlRaw($"CREATE VIEW vwOrderProducts AS {orderProductsQuerySql}");

ctx.Products.AddRange(
    new Product { Id = 1, EAN = "1234567890123", Name = "Coffee Mug", PriceEur = 9.99m },
    new Product { Id = 2, EAN = "9876543210987", Name = "Notebook", PriceEur = 4.50m },
    new Product { Id = 3, EAN = "5555555555555", Name = "Pen", PriceEur = 1.20m }
);

ctx.Orders.AddRange(
    new Order { Id = 1001, OrderNumber = "ORD-2024-0001" },
    new Order { Id = 1002, OrderNumber = "ORD-2024-0002" }
);

ctx.OrderRows.AddRange(
    new OrderRow { OrderId = 1001, ProductId = 1, Amount = 5 },
    new OrderRow { OrderId = 1001, ProductId = 2, Amount = 4 },
    new OrderRow { OrderId = 1002, ProductId = 3, Amount = 3 },
    new OrderRow { OrderId = 1002, ProductId = 2, Amount = 2 }
);

ctx.SaveChanges();

var orderProducts = ctx.Database.SqlQueryRaw<OrderProduct>("SELECT * FROM vwOrderProducts");
foreach (var op in orderProducts)
{
    Console.WriteLine($"{op.OrderNumber}, {op.EAN}: {op.Amount} PCS, {op.TotalPriceEur} EUR");
}
