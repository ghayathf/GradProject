using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TheNeqatcomApp.Core.Common;

namespace TheNeqatcomApp.Infra.Common
{
    public class DBContext : DbContext, IDBContext
    {
        private DbConnection _Connection;
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
                    _Connection.Open();
                }
                else if (_Connection.State != ConnectionState.Open)
                {
                    _Connection.Open();
                }
                return _Connection;
            }
        }
    }
}
