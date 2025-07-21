using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio.Sitad;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBRelSigef : DBBroker<RelSigef>
    {
        public static List<RelSigef> RecuperarItens(DateTime dtInicio, DateTime dtFim, string idTipoTributo)
        {
            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@dtInicio", dtInicio));
            parametros.Add(new SqlParameter("@dtFim", dtFim));
            parametros.Add(new SqlParameter("@idTributo", idTipoTributo));

            return ExecCmdSQL("dbo.usp_RelSIGEF", parametros, System.Data.CommandType.StoredProcedure);
        }

    }
}
