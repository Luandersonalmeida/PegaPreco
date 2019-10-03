using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace PegaPreco.Petrobras
{
    public class PegaPrecoPetrobras
    {
        private IWebDriver driver;

        public PegaPrecoPetrobras()
        {

        }

        [Test]
        public void ObterPrecoPetrobras()
        {
            LoginPetrobras();
        }
        public void LoginGenerico(string user, string pass, string centoCusto, string pathCentroCusto, string msgErro, string empresa, string site)
        {
            try
            {
                ChromeOptions chromeoptions = new ChromeOptions();
                chromeoptions.AddArguments("headless");
                driver = new ChromeDriver(chromeoptions);
                //driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                Thread.Sleep(5000);
                driver.Navigate().GoToUrl("https://cn.br-petrobras.com.br/login/login.jsf");
                Thread.Sleep(10000);
                driver.Navigate().Refresh();
                Thread.Sleep(10000);
                driver.FindElement(By.Id("usuario")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("senha")).SendKeys(pass);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("j_id1377415186_1_790d5166")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='HOME'])[1]/following::a[1]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Rascunhos dos Pedidos'])[1]/following::a[1]")).Click();
                Thread.Sleep(4000);
                driver.FindElement(By.XPath(pathCentroCusto)).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='CNPJ:'])[1]/following::input[3]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='HOME'])[1]/following::a[1]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Agendar'])[1]/preceding::a[1]")).Click();
                Thread.Sleep(1000);
                var elemento = driver.FindElement(By.ClassName("dv-formulario-tabela-produtos"));
                var linhas = elemento.FindElements(By.ClassName("linha-item-"));
                List<Produto> precos = new List<Produto>();
                foreach (var linha in linhas)
                {
                    string numero = linha.FindElement(By.ClassName("valor-numerico")).Text;
                    string produto = linha.FindElement(By.CssSelector("td > span")).Text;
                    precos.Add(new Produto(produto, numero, site, empresa, centoCusto));
                }
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Produto>));
                var path = $@"C:\Diretorio{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, precos);
                file.Close();
                driver.Quit();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message == "O tempo limite da operação foi atingido")
                {
                    msgErro = "A página não pôde ser carregada por que o servidor não deu retorno.";
                }


                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(TratamentoDeErro));
                var path = $@"C:\Diretorio{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, new TratamentoDeErro(msgErro, empresa, centoCusto));
                file.Close();
                driver.Quit();
            }
        }
        public void LoginPetrobras()
        {
            this.LoginGenerico(
                "",
                "",
                "",
                "",
                "",
                "",
                "Erro ao gerar dados");
        }
    }
}
