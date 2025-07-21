using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DBBroker.Engine;
using System.Data.Common;
using RemessaBB.FUNAF.Domain;

namespace RemessaBB.FUNAF.Persistence
{
    public class DBRemessaBoletoBB : DBBroker<RemessaBB_Boletos>
    {
        public static List<RemessaBB_Boletos> ListarParcelas(int IdRemessaBB)
        {
            try
            {
                var parametros = new List<DbParameter> { new SqlParameter("@IdRemessaBB", IdRemessaBB) };

                return ExecCmdSQL("SELECT " +
                                        "bb.idRemessaBoletos, " +
                                        "bb.idRemessaBB, " +
                                        "bb.idBoleto, " +
                                        "bb.vaDocumento, " +
                                        "bb.dtVencimento, " +
                                        "bb.idComando, " +
                                        "bb.nuCodRetorno, " +
                                        "bb.dtRetorno, " +
                                        "d.idServentia " +
                                    "FROM tbRemessaBB_Boletos bb " +
                                    "INNER JOIN tbDeclaracoes d ON d.idBoleto = bb.idBoleto " +
                                    "WHERE bb.idRemessaBB = @IdRemessaBB " +
                                    "AND dtRetorno IS NULL " +
                                    "ORDER BY bb.idRemessaBoletos ASC", parametros);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}