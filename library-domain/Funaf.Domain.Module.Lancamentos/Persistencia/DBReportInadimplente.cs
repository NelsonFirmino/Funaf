using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBReportInadimplente : DBBroker<ReportInadimplente>
    {
        public static List<ReportInadimplente> RecuperarItens(int nuAno)
        {
            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@Ano", nuAno));

            return ExecCmdSQL("dbo.usp_ReportInadimplentes", parametros, CommandType.StoredProcedure);
        }
    }
}
