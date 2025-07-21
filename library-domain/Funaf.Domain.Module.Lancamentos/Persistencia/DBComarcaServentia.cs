using System;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBComarcaServentia : DBBroker<ComarcaServentia>
    {
        public static ComarcaServentia GetByServentia(int id)
        {
            var query = "SELECT * FROM tbComarcas_Serventias " +
               "WHERE  idServentia = @id";

            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@id", id)
            };

            return ExecCmdSQL(query, parametros).FirstOrDefault();
        }
    }
}
