using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegaPreco
{
    public class TratamentoDeErro
    {
        public TratamentoDeErro()
        {
        }

        public TratamentoDeErro(string logerror, string empresa, string centroDeCusto)
        {
            LogErro = logerror;
            DataHora = DateTime.Now;
            CentroCusto = centroDeCusto;
            Empresa = empresa;
        }
        public string LogErro { get; set; }
        public DateTime DataHora { get; set; }
        public string CentroCusto { get; set; }
        public string Empresa { get; set; }
    }
    
}
