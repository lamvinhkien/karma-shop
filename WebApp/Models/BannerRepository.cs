using Dapper;
using System.Data;

namespace WebApp.Models;

public class BannerRepository : BaseRepository
{
    public BannerRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Banner> GetBanners()
    {
        return connection.Query<Banner>("SELECT TOP 2 * FROM Banner ORDER BY NEWID()");
    }

    public IEnumerable<Banner> GetBannersForDashboard()
    {
        return connection.Query<Banner>("SELECT * FROM Banner");
    }

    public int Add(Banner obj)
    {
        string sql = "INSERT INTO Banner(Name, Description, ImageURL) VALUES (@Name, @Description, @ImageURL)";
        return connection.Execute(sql, obj);
    }

    public Banner? GetBanner(byte id)
    {
        return connection.QuerySingleOrDefault<Banner>("SELECT * FROM Banner WHERE BannerId = @Id", new { id });
    }

    public int Edit(Banner obj)
    {
        string sql = "UPDATE Banner SET Name = @Name, Description = @Description, ImageURL = @ImageURL WHERE BannerId = @BannerId";
        return connection.Execute(sql, obj);
    }

    public int Delete(short id)
    {
        string sql = "SELECT ImageURL FROM Banner WHERE BannerId = @Id";
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
        sql = "DELETE FROM Banner WHERE BannerId = @Id";
        return connection.Execute(sql, new { Id = id });
    }
}

