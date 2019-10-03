using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PegaPreco.Shell
{
    public class PegaPrecoShell
    {
        private IWebDriver driver;

        public PegaPrecoShell()
        {

        }

        [Test]
        public void ObterPrecoShell()
        {
            LoginShell();
        }
        public void LoginGenerico(string user, string pass, string centoCusto, string msgErro, string site)
        {
            try
            {
                ChromeOptions chromeoptions = new ChromeOptions();
                chromeoptions.AddArguments("headless");
                driver = new ChromeDriver(chromeoptions);
                //driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.csonline.com.br/Pages/Login/Login.aspx?usid=e3d8ba925adc4d268de8cb8cfc63c6ad");
                Thread.Sleep(7000);
                driver.FindElement(By.Id("TextBoxCustomer")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("PIN")).SendKeys(pass);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("btnLogin")).Click();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Informações de Preços'])[1]/preceding::a[1]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Informações de Preços'])[1]/preceding::a[1]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("(//a[@id='lnkNoSubChild'])[7]")).Click();
                Thread.Sleep(2000);
                var tabela = driver.FindElement(By.ClassName("panel-group"));
                var itens = tabela.FindElements(By.ClassName("panel-default"));

                List<Produto> produtos = new List<Produto>();

                string empresa;
                string prod;
                string valor;

                Thread.Sleep(2000);

                foreach (var item in itens)
                {
                    item.FindElement(By.TagName("span")).Click();
                    Thread.Sleep(4000);
                    empresa = item.FindElement(By.TagName("span")).Text;

                    var tabelaProdutos = item.FindElement(By.TagName("table")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
                    try
                    {
                        if (string.IsNullOrEmpty(tabelaProdutos.First().FindElement(By.TagName("td")).Text))
                        {
                            Thread.Sleep(1000);
                            item.FindElement(By.TagName("span")).Click();
                            Thread.Sleep(1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("");
                    }

                    foreach (var produto in tabelaProdutos)
                    {
                        var colunas = produto.FindElements(By.TagName("td"));
                        prod = colunas[0].Text;
                        valor = colunas[1].Text;
                        valor = valor.Replace("R$ ", "");

                        produtos.Add(new Produto(prod, valor, site, empresa, centoCusto));
                    }
                }

                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Produto>));
                var path = $@"C:\Diretorio{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, produtos);
                file.Close();

                driver.Quit();
                Thread.Sleep(5000);

            }
            catch (Exception)
            {

                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(TratamentoDeErro));
                var path = $@"C:\Diretorio{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, new TratamentoDeErro(msgErro, site, centoCusto));
                file.Close();
                driver.Quit();
            }
        }
        public void LoginShell()
        {
            this.LoginGenerico(
                   "",
                   "",
                   "",
                   "",
                   "Erro ao gerar dados");
        }
    }
}


