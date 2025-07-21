using System;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBComarca : DBBroker<Comarca>
    {
        public static string RecuperarNomePorServentia(int idServentia)
        {
            var query = "SELECT c.txComarca " +
                        "FROM tbComarcas c " +
                        "INNER JOIN tbComarcas_Serventias cs ON cs.idComarca = c.idComarca " +
                        "WHERE cs.idServentia = @idServentia";

            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@idServentia", idServentia)
            };

            return ExecCmdSQL(query, parametros).FirstOrDefault().txComarca;
        }

        public static Comarca RecuperarPorServentia(int idServentia)
        {
            var query = "SELECT * " +
                        "FROM tbComarcas c " +
                        "INNER JOIN tbComarcas_Serventias cs ON cs.idComarca = c.idComarca " +
                        "WHERE cs.idServentia = @idServentia";

            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@idServentia", idServentia)
            };

            return ExecCmdSQL(query, parametros).FirstOrDefault();
        }
    }
}
