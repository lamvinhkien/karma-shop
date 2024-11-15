using Dapper;
using System.Data;

namespace WebApp.Models;

public class BrandRepository : BaseRepository
{
    public BrandRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Brand> GetBrands()
    {
        return connection.Query<Brand>("SELECT TOP 5 * FROM Brand ORDER BY NEWID()");
    }

    public IEnumerable<Brand> GetBrandsForDashboard()
    {
        return connection.Query<Brand>("SELECT * FROM Brand");
    }

    public int Add(Brand obj)
    {
        string sql = "INSERT INTO Brand(ImageURL) VALUES (@ImageURL)";
        return connection.Execute(sql, obj);
    }

    public Brand? GetBrand(byte id)
    {
        return connection.QuerySingleOrDefault<Brand>("SELECT * FROM Brand WHERE BrandId = @Id", new { id });
    }

    public int Edit(Brand obj)
    {
        string sql = "UPDATE Brand SET ImageURL = @ImageURL WHERE BrandId = @BrandId";
        return connection.Execute(sql, obj);
    }

    public int Delete(short id)
    {
        string sql = "SELECT ImageURL FROM Brand WHERE BrandId = @Id";
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
        sql = "DELETE FROM Brand WHERE BrandId = @Id";
        return connection.Execute(sql, new { Id = id });
    }
}

