using System.Data;
using Dapper;
using WebApp.Models;

namespace WebApp.Models;

public class InvoiceDetailRepository : BaseRepository
{
    public InvoiceDetailRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<InvoiceDetail> GetProductByInvoiceDetail(string id)
    {
        return connection.Query<InvoiceDetail>("SELECT InvoiceDetail.InvoiceId, InvoiceDetail.ProductId, InvoiceDetail.Quantity, InvoiceDetail.Price AS InvoicePrice, Product.Name, Product.Price AS ProductPrice, Product.ImageURL FROM  InvoiceDetail JOIN Product ON InvoiceDetail.ProductId = Product.ProductId WHERE InvoiceDetail.InvoiceId = @Id", new { id });
    }


}