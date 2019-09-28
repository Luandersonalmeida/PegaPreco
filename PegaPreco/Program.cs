using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PegaPreco.Ipiranga;
using PegaPreco.Petrobras;
using PegaPreco.Shell;

namespace PegaPreco
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Pega Preço");

            Console.WriteLine("Executando Pega Preço Ipiranga");
            new PegaPrecoIpiranga().ObterPrecoIpiranga();

            Console.WriteLine("Executando Pega Preço Shell");
            new PegaPrecoShell().ObterPrecoShell();

            Console.WriteLine("Executando Pega Preço Petrobras");
            var pPrecoPetrobras = new PegaPrecoPetrobras();

            try
            {
                pPrecoPetrobras.ObterPrecoPetrobras();
            }
            catch (Exception)
            {
                pPrecoPetrobras.ObterPrecoPetrobras();
            }

            Console.WriteLine("Finalizando Pega Preço.");

            Environment.Exit(0);
        }
    }
}
