using System.Data;
using Dapper;
using WebApp.Models;

public class CartRepository : BaseRepository
{
    public CartRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Cart> GetCarts(string code)
    {
        string sql = "SELECT Cart.*, Name, ImageUrl, Price FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId AND CartCode = @Code";
        return connection.Query<Cart>(sql, new { code });
    }

    public int Add(Cart obj)
    {
        return connection.Execute("AddCart",
            new { obj.CartCode, obj.ProductId, obj.Quantity },
            commandType: CommandType.StoredProcedure);
    }

    public int Edit(Cart obj)
    {
        string sql = "UPDATE Cart SET Quantity = @Quantity WHERE CartCode = @CartCode AND ProductId = @ProductId;";
        return connection.Execute(sql, new { obj.CartCode, obj.ProductId, obj.Quantity });
    }

    public int Delete(int id)
    {
        string sql = "DELETE FROM Cart WHERE ProductId = @id";
        return connection.Execute(sql, new { id });
    }
}
