using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegaPreco
{
    public class Produto
    {
        public Produto()
        {

        }
        
        public Produto(string nome, string preco, string tipo, string empresa, string centroDeCusto)
        {
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
            DataHora = DateTime.Now;
            Empresa = empresa;
            CentroCusto = centroDeCusto;
        }
        public string Nome { get; set; }
        public string Preco { get; set; }
        public string Tipo { get; set; }
        public DateTime DataHora { get; set; }
        public string CentroCusto { get; set; }
        public string Empresa { get; set; }
    }
}
