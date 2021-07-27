using System.Data;

namespace it.example.dotnetcore5.dal.dapper.Interfaces
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
