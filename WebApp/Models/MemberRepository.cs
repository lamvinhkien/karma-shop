using System.Data;
using Dapper;
using WebApp.Services;

namespace WebApp.Models;

public class MemberRepository : BaseRepository
{
    public MemberRepository(IDbConnection connection) : base(connection)
    {
    }

    public Member? Login(LoginModel obj)
    {
        string sql = "SELECT MemberId, Email, Name, GivenName, SurName, Role FROM Member WHERE Email = @Email AND Password = @Password";
        return connection.QuerySingleOrDefault<Member>(sql, new { Email = obj.Eml, Password = Helper.Hash(obj.Pwd) });
    }

    public IEnumerable<Member> GetMembers()
    {
        string sql = "SELECT MemberId, Email, Name, GivenName, SurName, Role FROM Member";
        return connection.Query<Member>(sql);
    }

    public int Add(Member obj)
    {
        return connection.Execute("AddMember", new
        {
            obj.MemberId,
            obj.Email,
            obj.Name,
            obj.Password,
            obj.GivenName,
            obj.SurName,
            Role = string.IsNullOrEmpty(obj.Role) ? Role.Customer.ToString() : obj.Role
        }, commandType: CommandType.StoredProcedure);
    }

    public Member? GetMember(string id)
    {
        return connection.QuerySingleOrDefault<Member>("SELECT * FROM Member WHERE MemberId = @Id", new { id });
    }

    public int Edit(Member obj)
    {
        string sql = "UPDATE Member SET Email = @Email, Name = @Name, GivenName = @GivenName, SurName = @SurName, Role = @Role WHERE MemberId = @MemberId";
        return connection.Execute(sql, obj);
    }

    public int Delete(string id)
    {
        string sql = "DELETE FROM Member WHERE MemberId = @id";
        return connection.Execute(sql, new { id });
    }
}