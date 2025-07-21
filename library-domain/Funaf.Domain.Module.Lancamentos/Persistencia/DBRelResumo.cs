using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBRelResumo : DBBroker<RelResumo>
    {
        public static List<RelResumo> RecuperarItens(int idDeclaracao)
        {
            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@idDeclaracao", idDeclaracao));

            return ExecCmdSQL("dbo.usp_RelResumo", parametros, System.Data.CommandType.StoredProcedure);
        }
    }
}
