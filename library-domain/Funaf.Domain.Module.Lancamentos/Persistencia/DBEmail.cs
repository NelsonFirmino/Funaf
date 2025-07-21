using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBEmail : DBBroker<Email>
    {
        public static List<Email> RecuperarDadosParaEnviarLoginESenhaTodos()
        {
            var query = "SELECT tbUsuarios.idUsuario, tbUsuarios.txNome, tbServentias.txCartorio, tbComarcas.txComarca, " +
                          "tbUsuarios.txCPF AS txUsuario, SUBSTRING(tbUsuarios.txCPF, 1, 6) AS txSenha, RTRIM(LTRIM(tbUsuarios.txEmail)) AS txEmail " +
                          "FROM tbUsuarios " +
                          "INNER JOIN tbServentias_Usuarios ON tbUsuarios.idUsuario = tbServentias_Usuarios.idUsuario " +
                          "INNER JOIN tbServentias ON tbServentias_Usuarios.idServentia = tbServentias.idServentia " +
                          "INNER JOIN tbComarcas_Serventias ON tbServentias.idServentia = tbComarcas_Serventias.idServentia " +
                          "INNER JOIN tbComarcas ON tbComarcas_Serventias.idComarca = tbComarcas.idComarca " +
                          "WHERE (tbUsuarios.isEmailEnviado = 0)";

            return ExecCmdSQL(query);
        }

        public static List<Email> ConfirmarEnvio(string email)
        {
            var query = "UPDATE tbUsuarios SET isEmailEnviado = 1 WHERE (RTRIM(LTRIM(txEmail)) = @txEmail)";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@txEmail", email)
            };

            return ExecCmdSQL(query, parametros);
        }
    }
}
