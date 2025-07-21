using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBReportBoletoEmAtraso : DBBroker<ReportBoletoEmAtraso>
    {
        public static List<ReportBoletoEmAtraso> RecuperarItens()
        {
            return ExecCmdSQL("dbo.usp_ReportBoletoEmAtraso");
        }
    }
}
