using System;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBServentia : DBBroker<Serventia>
    {
        public static List<Serventia> GetTodos()
        {
            var query = "SELECT  s.idServentia, s.txCartorio, s.txResponsavel, s.txEndereco,s.txBairro, " +
                        "s.txCEP,s.txCPF, s.txTelefone, s.txEmail, s.txServicos, s.dtCadastro,c.txComarca, c.* " +
                        "FROM tbServentias AS s " +
                        "LEFT JOIN tbComarcas_Serventias AS cs ON s.idServentia = cs.idServentia " +
                        "LEFT JOIN tbComarcas AS c ON c.idComarca = cs.idComarca  " +
                        "ORDER BY c.txComarca, s.txCartorio";

            var usuarios = new List<Serventia>();
            usuarios = CarregarComarca(ExecCmdSQL(query));

            return usuarios;

        }
        public static List<Serventia> ListarPorComarca(int idComarca)
        {
            var query = "SELECT  s.idServentia, s.txCartorio, s.txResponsavel, s.txEndereco, " +
                        "s.txCEP, s.txTelefone, s.txEmail, s.txServicos, s.dtCadastro " +
                        "FROM tbServentias AS s " +
                        "INNER JOIN tbComarcas_Serventias AS cs ON s.idServentia = cs.idServentia " +
                        "WHERE(cs.idComarca = @idComarca) " +
                        "ORDER BY s.txCartorio";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idComarca", idComarca)
            };

            return ExecCmdSQL(query, parametros);
        }

        public static List<Serventia> ListarPorServentia(int idUsuario)
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
        public static List<Serventia> PesquisarServentias(string cartorio,string responsavel )
        {
            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@responsavel", responsavel),
                new SqlParameter("@cartorio", cartorio)
            };
            var query = "SELECT * FROM tbServentias s " +
                        "LEFT JOIN tbComarcas_Serventias AS cs ON s.idServentia = cs.idServentia " +
                        "LEFT JOIN tbComarcas AS c ON c.idComarca = cs.idComarca WHERE (1 = 1)  ";
            if (!responsavel.Equals(""))
                query += " AND s.txResponsavel  like '%'+ @responsavel + '%'";
            if (!cartorio.Equals(""))
                query += " AND s.txCartorio like '%' + @cartorio + '%'";
            query += " ORDER BY c.txComarca, s.txCartorio";
            var usuarios = new List<Serventia>();
            usuarios = CarregarComarca(ExecCmdSQL(query, parametros));

            return usuarios;

        }
         private static List<Serventia> CarregarComarca (List<Serventia> serventias)
        {
            var usuarios = new List<Serventia>();
            foreach (var iUsuario in serventias)
            {
                iUsuario.Comarca = DBComarca.RecuperarPorServentia(iUsuario.Id);
                usuarios.Add(iUsuario);
            }
            return usuarios;
        }
    }
}
