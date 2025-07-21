using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;


namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBRelDevolucaoSolicitacao : DBBroker<RelDevolucaoSolicitacao>
    {
        public static List<RelDevolucaoSolicitacao> RecuperarItens(int idSolicitacao)
        {
            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@idSolicitacao", idSolicitacao));

            return ExecCmdSQL("dbo.usp_RelSolicitacao_Memorando", parametros, System.Data.CommandType.StoredProcedure);
        }
    }
}
