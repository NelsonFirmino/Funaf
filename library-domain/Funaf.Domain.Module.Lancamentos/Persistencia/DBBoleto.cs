using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBBoleto : DBBroker<Boleto>
    {
        /// <summary>
        /// Este método não deve ser invocado para salvar boleto
        /// </summary>
        /// <param name="boleto"></param>
        new public static void Save(Boleto boleto)
        {
            throw new NotImplementedException("Não é permitido salvar o boleto diretamente, utilizar a chamada para o método GerarBoleto(idDeclaracao)");
        }

        public static Boleto GerarBoleto(int idDeclaracao)
        {
            var parametros = new List<DbParameter> { new SqlParameter("@idDeclaracao", idDeclaracao) };

            var boleto = ExecCmdSQL("dbo.usp_Boleto_GerarBoleto", parameters: parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return boleto;
        }

        public static string RegistrarBoleto(int idBoleto)
        {
            var parametros = new List<DbParameter> { new SqlParameter("@idBoleto", idBoleto) };

            var boleto = ExecCmdSQL("dbo.usp_Boleto_RegistrarBoleto", parameters: parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return boleto.Id.ToString();
        }

        public static Boleto RegistrarBoletoWS(int idBoleto)
        {
            var parametros = new List<DbParameter> { new SqlParameter("@idBoleto", idBoleto) };

            var boleto = ExecCmdSQL("dbo.usp_Boleto_RegistrarBoletoWS", parameters: parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return boleto;
        }

        public static DateTime RecuperarVencimento(int idBoleto)
        {
            var parametros = new List<DbParameter> { new SqlParameter("@idBoleto", idBoleto) };

            var boleto = ExecCmdSQL("dbo.usp_Boleto_RegistrarBoleto", parameters: parametros, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return boleto.dtVencimento;
        }

        public static void AtualizarNossoNumero(int idRegistroBBWSBoleto, string textoNumeroTituloCliente)
        {
            var parametros = new List<DbParameter>
            {
                new SqlParameter("@idRegistroBBWSBoleto", idRegistroBBWSBoleto),
                new SqlParameter("@txNossoNumero", textoNumeroTituloCliente)
            };

            ExecCmdSQL("UPDATE tbRegistroBBWS_Boletos " +
                                    "SET txNossoNumero = @txNossoNumero " +
                                    "WHERE (idRegistroBBWSBoleto = @idRegistroBBWSBoleto)",
                        parameters: parametros, commandType: CommandType.Text);

        }
    }
}
