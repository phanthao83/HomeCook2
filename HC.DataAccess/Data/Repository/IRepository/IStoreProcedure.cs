using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace HC.DataAccess.Data.Repository.IRepository
{
   public interface IStoreProcedure : IDisposable
       
    {
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);

        void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);

        T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null);
    }
}



