using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PegaPreco.Ipiranga
{
    public class PegaPrecoIpiranga
    {
        private IWebDriver driver;


        public PegaPrecoIpiranga()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ObterPrecoIpiranga()
        {
            LoginEmpresa();
        }

        public void LoginGenerico(string user, string pass, string centoCusto, string pathCentroCusto, string msgErro, string empresa, string site)
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.redeipiranga.com.br/wps/portal/redeipiranga/login/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOKNAiwtDI1NDL0sgk1MDBzdw5zMg3y8DA1CTIAKIoEKDHAARwNC-sP1o_AqCTSHKsBjRUFuhEGmo6IiALSd4v0!/dl5/d5/L2dBISEvZ0FBIS9nQSEh/");
                Thread.Sleep(4000);
                driver.FindElement(By.Id("viewns_Z7_2P981341J8S440AGVB7RLJ1034_:ns_Z7_2P981341J8S440AGVB7RLJ1034_j_id1359681328_510b177e:login")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("viewns_Z7_2P981341J8S440AGVB7RLJ1034_:ns_Z7_2P981341J8S440AGVB7RLJ1034_j_id1359681328_510b177e:senha")).SendKeys(pass);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("viewns_Z7_2P981341J8S440AGVB7RLJ1034_:ns_Z7_2P981341J8S440AGVB7RLJ1034_j_id1359681328_510b177e:ns_Z7_2P981341J8S440AGVB7RLJ1034_j_id1359681328_510b17ac")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.Id("formEstabelecimentos")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath(pathCentroCusto)).Click();
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://www.redeipiranga.com.br/wps/myportal/redeipiranga/minhasfinancas/combustivel/condicoescomerciais/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOItjDw8DSycDbwtwvxdDBxNjV1DPUOcDQ28zIAKIoEKjAIsLQyNTQy9LIJNTAwc3cOczIN8vAwNAs3x6g8xheo3wAEcDcD6cRnvY0jAfqACouzHo4CA_8P1o_A6ERQCYAX4vIhmiY-PMdgSV18fZwMDI2P9gtzQ0NAIg0xPR0VFAJZpewQ!/dl5/d5/L2dBISEvZ0FBIS9nQSEh/?uri=nm%3Aoid%3AZ6_82HI08C0K8VOD0A53EUITC10J6");
                Thread.Sleep(4000);

                List<Produto> produtos = new List<Produto>();
                Thread.Sleep(7000);
                var frame = driver.FindElements(By.TagName("iframe"));
                driver.SwitchTo().Frame(frame[0]);

                var tabela = driver.FindElement(By.ClassName("centdig_tabela_lista_produtos"));
                var linhas = driver.FindElements(By.TagName("tr"));

                foreach (var linha in linhas)
                {
                    try
                    {
                        var colunas = linha.FindElements(By.TagName("td"));

                        if (!string.IsNullOrEmpty(colunas[0].Text) && !string.IsNullOrEmpty(colunas[2].Text))
                        {
                            produtos.Add(new Produto(colunas[0].Text, colunas[2].Text, site, empresa, centoCusto));
                        }

                    }
                    catch (Exception ex)
                    {
                        //nada
                    }
                }

                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Produto>));
                var path = $@"C:\Diretorio{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, produtos);
                file.Close();
                driver.Quit();
            }
            catch (Exception)
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(TratamentoDeErro));
                var path = $@"C:\Diretorio{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);
                Thread.Sleep(1000);
                writer.Serialize(file, new TratamentoDeErro(msgErro, empresa, centoCusto));
                Thread.Sleep(1000);
                file.Close();
                Thread.Sleep(1000);
                driver.Quit();
            }
        }
        public void LoginEmpresa()
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