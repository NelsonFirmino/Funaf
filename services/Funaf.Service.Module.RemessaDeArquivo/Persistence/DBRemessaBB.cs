using DBBroker.Engine;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace RemessaBB.FUNAF.Persistence
{
    public class DBRemessaBB : DBBroker<Domain.RemessaBB>
    {
        public static Domain.RemessaBB RecuperarRemessaPorId(int idRemessaBB)
        {
            try
            {
                var parametros = new List<DbParameter> { new SqlParameter("@IdRemessaBB", idRemessaBB) };

                var lista = ExecCmdSQL("SELECT TOP 1 rb.* " +
                                        "FROM tbRemessaBB rb " +
                                        "WHERE rb.idRemessaBB = @IdRemessaBB " +
                                        "ORDER BY rb.idRemessaBB DESC", parametros);

                return lista.Count > 0 ? lista[0] : null;
            }
            catch (Exception ex)
            {
                var t = ex;
                return null;
            }
        }

        public static Domain.RemessaBB RecuperarProximaRemessa()
        {
            try
            {               
                var lista = ExecCmdSQL("EXEC dbo.usp_RemessaBB_GerarRemessa5480");

                //Console.WriteLine(lista.Count);

                return lista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void RegistrarEnvio(string filename, Domain.RemessaBB remessa)
        {
            var parametros = new List<DbParameter>
            {
                new SqlParameter("@nuSequencial", remessa.nuSequencial),
                new SqlParameter("@idRemessaBB", remessa.Id),
                new SqlParameter("@txArquivo", filename)
            };

            ExecCmdSQL("UPDATE tbRemessaBB SET nuSequencial = @nuSequencial,  dtEnvio = GETDATE(), txArquivo = @txArquivo WHERE (idRemessaBB = @idRemessaBB);", parametros);
        }
    }
}