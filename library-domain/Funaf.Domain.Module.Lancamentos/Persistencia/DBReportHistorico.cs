using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBReportHistorico : DBBroker<ReportHistorico>
    {
        public static List<ReportHistorico> RecuperarItens(int idServentia, DateTime dtInicial, DateTime dataFinal )
        {
            var parametros = new List<DbParameter>();
           
            parametros.Add(new SqlParameter("@dtInicio", dtInicial));
            parametros.Add(new SqlParameter("@dtFim", dataFinal));
            parametros.Add(new SqlParameter("@idServentia", idServentia));

            return ExecCmdSQL("dbo.usp_RelHistorico", parametros, CommandType.StoredProcedure);
        }
    }
}
