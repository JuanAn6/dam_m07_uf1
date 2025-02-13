using System;
using System.Data;
using System.Data.Common;

namespace DB
{
    internal class DBUtils
    {
        internal static void createParam(DbCommand consulta, object param_value, string param_name, DbType type)
        {
            var param = consulta.CreateParameter();
            param.ParameterName = param_name;
            param.Value = param_value;
            param.DbType = type;
            consulta.Parameters.Add(param);
        }
    }
}