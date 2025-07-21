using DBBroker.Mapping;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "tbServentias", PrimaryKey = "idServentia")]
    public class Email
    {
        public int Id { get; set; }

        public string txCartorio { get; set; }

        public string txComarca { get; set; }

        public string txNome { get; set; }

        public string txUsuario { get; set; }

        public string txSenha { get; set; }

        public string txEmail { get; set; }
    }
}