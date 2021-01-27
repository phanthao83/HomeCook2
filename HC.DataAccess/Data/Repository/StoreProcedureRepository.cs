using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using HC.DataAccess;
using HC.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace HC.DataAccess.Data.Repository
{
    public class StoreProcedureRepository : IStoreProcedure
    {
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public StoreProcedureRepository(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }



        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
