using System;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBDeclaracaoServico : DBBroker<DeclaracaoServico>
    {
        public static decimal RecuperarValorTotal(int idDeclaracao)
        {
            var query = "SELECT SUM(vaTotal) AS vaTotal " +
                        "FROM tbDeclaracoes_Servicos " +
                        "WHERE(idDeclaracao = @idDeclaracao)";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idDeclaracao", idDeclaracao)
            };

            return ExecCmdSQL(query, parametros).FirstOrDefault().vaTotal;
        }
    }
}
