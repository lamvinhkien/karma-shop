using Dapper;
using System.Data;

namespace WebApp.Models;

public class ProductRepository : BaseRepository
{
    public ProductRepository(IDbConnection connection) : base(connection)
    {
    }

    public Product? GetProductClient(int id)
    {
        string sql = "SELECT * FROM Product WHERE ProductId = @Id";
        return connection.QuerySingleOrDefault<Product>(sql, new { id });
    }

    public IEnumerable<Product> GetProducts()
    {
        return connection.Query<Product>("SELECT * FROM Product");
    }

    public IEnumerable<Product> GetLastestProducts()
    {
        return connection.Query<Product>("SELECT TOP 8 * FROM Product ORDER BY NEWID()");
    }

    public IEnumerable<Product> GetDealOfWeek()
    {
        return connection.Query<Product>("SELECT TOP 6 * FROM Product ORDER BY NEWID()");
    }

    public IEnumerable<Product> GetExclusiveDeal()
    {
        return connection.Query<Product>("SELECT TOP 2 * FROM Product ORDER BY NEWID()");
    }

    public IEnumerable<Product> GetProductsForDashboard()
    {
        return connection.Query<Product>("SELECT * FROM Product");
    }

    public int Add(Product obj)
    {
        string sql = "INSERT INTO Product(Name, Price, ImageURL, Availability, Description, DetailDescription, Specification, CategoryId) VALUES (@Name, @Price, @ImageURL, @Availability, @Description, @DetailDescription, @Specification, @CategoryId)";
        return connection.Execute(sql, obj);
    }

    public Product? GetProduct(byte id)
    {
        return connection.QuerySingleOrDefault<Product>("SELECT * FROM Product WHERE ProductId = @Id", new { id });
    }

    public int Edit(Product obj)
    {
        string sql = "UPDATE Product SET Name = @Name, Price = @Price, ImageURL = @ImageURL, Availability = @Availability, Description = @Description, DetailDescription = @DetailDescription, Specification = @Specification, CategoryId = @CategoryId WHERE ProductId = @ProductId";
        return connection.Execute(sql, obj);
    }

    public int Delete(short id)
    {
        string sql = "SELECT ImageURL FROM Product WHERE ProductId = @Id";
        string imageUrl;
        imageUrl = connection.QuerySingleOrDefault<string>(sql, new { Id = id });

        // Xóa file hình ảnh nếu có
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        // Xóa bản ghi khỏi cơ sở dữ liệu
        sql = "DELETE FROM Product WHERE ProductId = @Id";
        return connection.Execute(sql, new { Id = id });
    }
}

