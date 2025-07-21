using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBRelRecolhimento : DBBroker<RelRecolhimento>
    {
        public static List<RelRecolhimento> RecuperarItens(DateTime dtInicio, DateTime dtFinal){

            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@dtInicio", dtInicio));
            parametros.Add(new SqlParameter("@dtFim", dtFinal));


            return ExecCmdSQL("usp_RelRecolhimento", parametros, System.Data.CommandType.StoredProcedure);
        }
    }
}
