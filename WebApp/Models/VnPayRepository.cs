using System.Data;
using Dapper;
using WebApp.Services;

namespace WebApp.Models;

public class VnPayRepository : BaseRepository
{
    public VnPayRepository(IDbConnection connection) : base(connection)
    {

    }
    public int Add(VnPayResponse obj){
        return connection.Execute("AddVnPay", obj, commandType: CommandType.StoredProcedure);
    }
}