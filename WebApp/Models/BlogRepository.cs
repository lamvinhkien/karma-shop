using Dapper;
using System.Data;

namespace WebApp.Models;

public class BlogRepository : BaseRepository
{
    public BlogRepository(IDbConnection connection) : base(connection)
    {
    }

    public Blog? GetBlogClient(int id)
    {
        string sql = "SELECT * FROM Blog WHERE BlogId = @Id";
        return connection.QuerySingleOrDefault<Blog>(sql, new { id });
    }

    public IEnumerable<Blog> GetBlogs()
    {
        return connection.Query<Blog>("SELECT * FROM Blog");
    }

     public IEnumerable<Blog> GetBlogsForDashboard()
    {
        return connection.Query<Blog>("SELECT * FROM Blog");
    }

    public int Add(Blog obj)
    {
        string sql = "INSERT INTO Blog(ImageURL, BlogName, Author, Tag, Description, DetailDescription) VALUES (@ImageURL, @BlogName, @Author, @Tag, @Description, @DetailDescription)";
        return connection.Execute(sql, obj);
    }

    public Blog? GetBlog(byte id)
    {
        return connection.QuerySingleOrDefault<Blog>("SELECT * FROM Blog WHERE BlogId = @Id", new { id });
    }

    public int Edit(Blog obj)
    {
        string sql = "UPDATE Blog SET ImageURL = @ImageURL, BlogName = @BlogName, Author = @Author, Tag = @Tag, Description = @Description, DetailDescription = @DetailDescription WHERE BlogId = @BlogId";
        return connection.Execute(sql, obj);
    }

    public int Delete(short id)
    {
        string sql = "SELECT ImageURL FROM Blog WHERE BlogId = @Id";
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
        sql = "DELETE FROM Blog WHERE BlogId = @Id";
        return connection.Execute(sql, new { Id = id });
    }
}

