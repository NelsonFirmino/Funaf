using System;
using System.Collections.Generic;
using DBBroker.Engine;
using RemessaBB.FUNAF.Domain;

namespace RemessaBB.FUNAF.Persistence
{
    public class DBRemessaRetornoBB : DBBroker<RemessaBB_Retorno>
    {
        public static List<RemessaBB_Retorno> ListarTodos()
        {
            try
            {
                var lista = new List<RemessaBB_Retorno>();

                lista = ExecCmdSQL("SELECT rr.* FROM tbRemessaBB_Retorno rr " +
                                       "ORDER BY rr.idRemessaRetorno ASC");

                return lista;
            }
            catch (Exception ex)
            {
                var text = ex.Message;
                return null;
            }
        }
    }
}