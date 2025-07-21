using DBBroker.Engine;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Funaf.Domain.Module.Lancamentos.Persistencia
{
    public class DBUsuario : DBBroker<Usuario>
    {
        public static Usuario Autenticar(string cpf, string senha)
        {
            var query = "SELECT * " +
                "FROM tbUsuarios " +
                "WHERE PWDCOMPARE(@Senha, txSenha) = 1 AND  isAtivo = 1 AND txCPF = @CPF";

            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@CPF", cpf),
                new SqlParameter("@Senha", senha)
            };

            return ExecCmdSQL(query, parametros).FirstOrDefault();
        } 
        public static void Incluir(Usuario tb)
        {

            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@txNome", tb.txNome));
            parametros.Add(new SqlParameter("@txCPF", tb.txCPF));
            parametros.Add(new SqlParameter("@txEmail", tb.txEmail));
            parametros.Add(new SqlParameter("@txTelefone", tb.txTelefone));
            parametros.Add(new SqlParameter("@isAtivo", tb.isAtivo));
            parametros.Add(new SqlParameter("@isEmailEnviado", tb.isEmailEnviado));
            parametros.Add(new SqlParameter("@txFuncao", tb.txFuncao));

            var query = "INSERT INTO tbUsuarios(txNome, txCPF, txSenha, txEmail, txTelefone, isAtivo, isEmailEnviado, txFuncao) ";
            query += " VALUES (@txNome, @txCPF, dbo.ufn_CriptografarSenha(LEFT(@txCPF, 6)), @txEmail, @txTelefone, @isAtivo, @isEmailEnviado, @txFuncao);";
            ExecCmdSQL(query, parametros);

        }
        public static void Alterar(Usuario tb)
        {

            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@id", tb.Id));
            parametros.Add(new SqlParameter("@txNome", tb.txNome));
            parametros.Add(new SqlParameter("@txCPF", tb.txCPF));
            parametros.Add(new SqlParameter("@txEmail", tb.txEmail));
            parametros.Add(new SqlParameter("@txTelefone", tb.txTelefone));
            parametros.Add(new SqlParameter("@isAtivo", tb.isAtivo));
            parametros.Add(new SqlParameter("@isEmailEnviado", tb.isEmailEnviado));
            parametros.Add(new SqlParameter("@txFuncao", tb.txFuncao));

            var query = "UPDATE tbUsuarios SET txNome = @txNome , txCPF = @txCPF,  ";
            query += " txSenha = dbo.ufn_CriptografarSenha(LEFT(@txCPF, 6)), txEmail = @txEmail,";
            query += " txTelefone = @txTelefone,isAtivo = @isAtivo, isEmailEnviado = @isEmailEnviado, ";
            query += " txFuncao = @txFuncao WHERE idUsuario= @id;";
            ExecCmdSQL(query, parametros);

        }
        public static void AlterarSenha(int Id, string txSenha)
        {

            var parametros = new List<DbParameter>();
            parametros.Add(new SqlParameter("@id", Id));
            parametros.Add(new SqlParameter("@txSenha", txSenha));

            var query = "UPDATE tbUsuarios SET   ";
            query += " txSenha = dbo.ufn_CriptografarSenha(@txSenha)";
            query += " WHERE idUsuario= @id;";
            ExecCmdSQL(query, parametros);

        }
        public static List<Usuario> PesquisarUsuarios(string nome, string cpf)
        {
           
            var parametros = new System.Collections.Generic.List<DbParameter>()
            {
                new SqlParameter("@CPF", cpf),
                new SqlParameter("@nome", nome)
            };
            var query = "SELECT * FROM tbUsuarios WHERE (1 = 1)  ";
            if (!cpf.Equals(""))
                query += " AND txCPF = @CPF ";
            if (!nome.Equals(""))
                query += " AND txNome like '%' + @nome + '%'";

            return ExecCmdSQL(query, parametros);

        }

        public static Usuario PesquisarPorCpfOuEmail(string cpfOuEmail)
        {
            var query = @"SELECT TOP 1 * 
                  FROM tbUsuarios 
                  WHERE txCPF = @cpfOuEmail 
                     OR txEmail = @cpfOuEmail";
            var parametros = new List<DbParameter>
    {
        new SqlParameter("@cpfOuEmail", cpfOuEmail)
        {

        }
            };

            var lista = ExecCmdSQL(query, parametros);
            return lista.FirstOrDefault();
        }


        private static Usuario ExecFirst(string query, List<DbParameter> parametros)
        {
            throw new NotImplementedException();
        }

        private string GerarSenhaProvisoria()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

    }
}
