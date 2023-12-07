using System.Data;

namespace Library.Api.Data
{
    public interface IDbConnectionFactory
    {
        // DB connections to SQLite or other DBes
        Task<IDbConnection> CreateConnectionAsync();
    }
}
