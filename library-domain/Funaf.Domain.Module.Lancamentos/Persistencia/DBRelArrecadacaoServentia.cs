using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBRelArrecadacaoServentia : DBBroker<RelArrecadacaoServentia>
    {
        public static List<RelArrecadacaoServentia> RecuperarItens(DateTime dtInicio, DateTime dtFinal)
        {

            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@dtInicio", dtInicio));
            parametros.Add(new SqlParameter("@dtFim", dtFinal));


            return ExecCmdSQL("usp_RelArrecadacaoServentia", parametros, System.Data.CommandType.StoredProcedure);
        }
    }
}
