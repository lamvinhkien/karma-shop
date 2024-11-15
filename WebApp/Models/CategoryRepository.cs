using Dapper;
using System.Data;

namespace WebApp.Models;

public class CategoryRepository : BaseRepository
{
    public CategoryRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Category> GetCategorys()
    {
        return connection.Query<Category>("SELECT * FROM Category");
    }

    public IEnumerable<Category> GetCategorysForDashboard()
    {
        return connection.Query<Category>("SELECT * FROM Category");
    }

    public int Add(Category obj)
    {
        string sql = "INSERT INTO Category(Name, ImageURL, BackgroundColor) VALUES (@Name, @ImageURL, @BackgroundColor)";
        return connection.Execute(sql, obj);
    }

    public Category? GetCategory(byte id)
    {
        return connection.QuerySingleOrDefault<Category>("SELECT * FROM Category WHERE CategoryId = @Id", new { id });
    }

    public Category? GetCategoryName(int id)
    {
        return connection.QuerySingleOrDefault<Category>("SELECT Category.Name FROM Product JOIN Category ON Product.CategoryId = Category.CategoryId WHERE Product.ProductId = @Id", new { id });
    }

    public int Edit(Category obj)
    {
        string sql = "UPDATE Category SET Name = @Name, ImageURL = @ImageURL, BackgroundColor = @BackgroundColor WHERE CategoryId = @CategoryId";
        return connection.Execute(sql, obj);
    }

    public int Delete(short id)
    {
        string sql = "SELECT ImageURL FROM Category WHERE CategoryId = @Id";
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
        sql = "DELETE FROM Category WHERE CategoryId = @Id";
        return connection.Execute(sql, new { Id = id });
    }
}

