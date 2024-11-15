using System.Data;
using Dapper;

namespace WebApp.Models;

public class InvoiceRepository : BaseRepository
{
    public InvoiceRepository(IDbConnection connection) : base(connection)
    {
    }
    public IEnumerable<MonthSales> GetSalesByMonth(int year)
    {
        return connection.Query<MonthSales>("GetSalesByMonth", new { year }, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<CategorySales> GetSalesByCategory(int year)
    {
        return connection.Query<CategorySales>("GetSalesByCategory", new { year }, commandType: CommandType.StoredProcedure);
    }

    public decimal GetSalesMonthly(int year)
    {
        return connection.ExecuteScalar<decimal>("GetSalesMonthly", new { Year = year }, commandType: CommandType.StoredProcedure);
    }

    public int Add(Invoice obj)
    {
        obj.InvoiceDate = DateTime.Now;
        obj.InvoiceId = Guid.NewGuid().ToString().Replace("-", "");
        return connection.Execute("AddInvoice", new
        {
            obj.Address,
            obj.CartCode,
            obj.City,
            obj.Country,
            obj.Email,
            obj.FirstName,
            obj.InvoiceDate,
            obj.InvoiceId,
            obj.State,
            obj.PostCode,
            obj.Phone,
            obj.Note,
            obj.LastName
        }, commandType: CommandType.StoredProcedure);
    }

    public decimal GetAmount(string id)
    {
        return connection.ExecuteScalar<decimal>("GetInvoiceAmount", new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    public Invoice? GetInvoice(string id)
    {
        return connection.QuerySingleOrDefault<Invoice>("SELECT * FROM Invoice WHERE InvoiceId = @Id", new { id });
    }

    public IEnumerable<Invoice> GetInvoices()
    {
        string sql = "SELECT * FROM Invoice";
        return connection.Query<Invoice>(sql);
    }

    public int Delete(string id)
    {
        string query = @"
        BEGIN TRANSACTION;

        DELETE FROM InvoiceDetail
        WHERE InvoiceId = @InvoiceId;

        DELETE FROM Invoice
        WHERE InvoiceId = @InvoiceId;

        COMMIT TRANSACTION;";

        return connection.Execute(query, new { InvoiceId = id });
    }
}