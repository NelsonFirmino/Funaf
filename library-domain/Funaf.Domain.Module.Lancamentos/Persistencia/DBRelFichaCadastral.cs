using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBRelFichaCadastral : DBBroker<RelFichaCadastral>
    {
        public static List<RelFichaCadastral> RecuperarItens(int idServentia)
        {
            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@idServentia", idServentia));

            return ExecCmdSQL("usp_RelFichaCadastral", parametros, System.Data.CommandType.StoredProcedure);
        }
    }
}
