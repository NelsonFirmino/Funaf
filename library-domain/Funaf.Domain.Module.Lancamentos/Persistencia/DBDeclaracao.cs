using System;
using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Data.Common;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBDeclaracao : DBBroker<Declaracao>
    {
        public static List<Declaracao> ListagemDeclaracoesSimplificada(int idServentia)
        {
            var query = "SELECT d.* " +
                        "FROM tbDeclaracoes d " +
                        "WHERE(idServentia = @idServentia) " +
                        "ORDER BY dtPeriodoInicial DESC";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idServentia", idServentia)
            };

            return ExecCmdSQL(query, parametros);
        }

        public static bool VerificarPeriodo(Declaracao novaDeclaracao)
        {
            var query = "SELECT  * FROM tbDeclaracoes " +
                        "WHERE(idServentia = @idServentia) " +
                        "AND ((@dtInicio BETWEEN dtPeriodoInicial AND dtPeriodoFinal) " +
                        "OR (@dtFinal BETWEEN dtPeriodoInicial AND dtPeriodoFinal))";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idServentia", novaDeclaracao.Serventia.Id),
                new SqlParameter("@dtInicio", novaDeclaracao.dtPeriodoInicial),
                new SqlParameter("@dtFinal", novaDeclaracao.dtPeriodoFinal)
            };

            return ExecCmdSQL(query, parametros).Count <= 0;
        }

        public static List<Declaracao> ListarPorBoleto(int idBoleto)
        {
            var query = "SELECT * " +
                        "FROM tbDeclaracoes " +
                        "WHERE(idBoleto = @idBoleto) ";

            var parametros = new List<DbParameter>()
            {
                new SqlParameter("@idBoleto", idBoleto)
            };

            return ExecCmdSQL(query, parametros);
        }
    }
}
