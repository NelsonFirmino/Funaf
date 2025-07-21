using System;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBServentiaUsuario : DBBroker<ServentiaUsuario>
    {
        public static List<ServentiaUsuario> GetTodos()
        {
            var query = "SELECT  s.idServentia, s.txCartorio, s.txResponsavel, s.txEndereco,s.txBairro, " +
                        "s.txCEP,s.txCPF, s.txTelefone, s.txEmail, s.txServicos, s.dtCadastro,c.txComarca " +
                        "FROM tbServentias AS s " +
                        "INNER JOIN tbComarcas_Serventias AS cs ON s.idServentia = cs.idServentia " +
                        "INNER JOIN tbComarcas AS c ON c.idComarca = cs.idComarca  " +
                        "ORDER BY c.txComarca, s.txCartorio";

            return ExecCmdSQL(query);
        }
        public static List<ServentiaUsuario> PesquisarServentiaUsuarios(int idServentia, int idUsuario)
        {
            var query = "SELECT su.*,u.*,c.txComarca,s.* " +
                        "FROM tbServentias_Usuarios AS su " +
                        "INNER JOIN tbServentias AS s ON su.idServentia = s.idServentia " +
                        "INNER JOIN tbComarcas_Serventias AS cs ON s.idServentia = cs.idServentia " +
                        "INNER JOIN tbComarcas AS c ON c.idComarca = cs.idComarca  " +
                        "INNER JOIN tbUsuarios   AS u ON su.idUsuario = u.idUsuario " +
                        "WHERE (su.idServentia = @idServentia)  or (su.idUsuario = @idUsuario)" +
                        "ORDER BY c.txComarca,s.txCartorio";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idServentia", idServentia),
                new SqlParameter("@idUsuario", idUsuario)
            };

            var usuarios = new List<ServentiaUsuario>();
            foreach (var iUsuario in ExecCmdSQL(query, parametros))
            {
                iUsuario.Serventia.Comarca = DBComarca.RecuperarPorServentia(iUsuario.Serventia.Id);
                usuarios.Add(iUsuario);
            }   

            return usuarios;
        }

        public static List<ServentiaUsuario> ListarPorServentia(int idUsuario)
        {
            var query = "SELECT s.idServentia, s.txCartorio, s.txResponsavel, s.txEndereco, " +
                        "s.txCEP, s.txTelefone, s.txEmail, s.txServicos, s.dtCadastro " +
                        "FROM tbServentias AS s " +
                        "INNER JOIN tbServentias_Usuarios AS su ON s.idServentia = su.idServentia " +
                        "WHERE(su.idUsuario = @idUsuario)";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idUsuario", idUsuario)
            };

            return ExecCmdSQL(query, parametros);
        }
        public static List<ServentiaUsuario> PesquisarServentias(string responsavel, string cartorio)
        {

            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@responsavel", responsavel),
                new SqlParameter("@cartorio", cartorio)
            };
            var query = "SELECT * FROM tbUsuarios WHERE (1 = 1)  ";
            if (!responsavel.Equals(""))
                query += " AND txResponsavel  like '%'  @responsavel + '%'";
            if (!cartorio.Equals(""))
                query += " AND txCartorio like '%' + @cartorio + '%'";

            return ExecCmdSQL(query, parametros);

        }
    }
}
