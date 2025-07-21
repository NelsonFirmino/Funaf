using DBBroker.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(PrimaryKey = "IdDeclaracao", Table = "tbRelatorio")]
    public class RelArrecadacaoServentia
    {
        public string txComarca { get; set; }
        public string txCartorio { get; set; }
        public string txPeriodo { get; set; }
        public decimal vaDevido { get; set; }
        public decimal vaPago { get; set; }

    }
}
