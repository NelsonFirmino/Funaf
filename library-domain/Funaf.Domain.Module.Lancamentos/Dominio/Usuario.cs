using DBBroker.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbUsuarios", PrimaryKey = "idUsuario")]
    public class Usuario
    {
        public int Id { get; set; }

        [DBTransient]
        public List<Serventia> Cartorios { get; set; }

        public string txNome { get; set; }

        public string txCPF { get; set; }

        public string txFuncao { get; set; }

        [DBTransient]
        public string txSenha { get; set; }

        public string txEmail { get; set; }

        public string txTelefone { get; set; }

        public bool isAtivo { get; set; }

        public bool isEmailEnviado { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }
    }
}
