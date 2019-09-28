using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegaPreco
{
    class Empresa
    {
        public Empresa(string nome)
        {
            Nome = nome;
            Produtos = new List<Produto>();
        }

        public string Nome { get; set; }

        public List<Produto> Produtos { get; set; }

        public void AdicionarProduto(string nome, string preco)
        {          
        }
    }
}
