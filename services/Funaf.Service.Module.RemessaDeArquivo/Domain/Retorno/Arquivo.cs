using System.Collections.Generic;

namespace RemessaBB.FUNAF.Domain.Retorno
{
    public class Arquivo
    {
        public Header Header { get; set; }
        public List<Detalhe> Detalhes { get; set; }
        public Trailler Trailler { get; set; }
    }
}