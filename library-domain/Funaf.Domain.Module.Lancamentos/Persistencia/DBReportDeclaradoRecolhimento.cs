using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBReportDeclaradoRecolhimento : DBBroker<ReportDeclaradoRecolhimento>
    {
        public static List<ReportDeclaradoRecolhimento> RecuperarItens(int nuAno, int idServentia)
        {
            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@Ano", nuAno));
            parametros.Add(new SqlParameter("@idServentia", idServentia));

            return ExecCmdSQL("dbo.usp_ReportDeclaracaoArrecadacao", parametros, CommandType.StoredProcedure);
        }
    }
}
