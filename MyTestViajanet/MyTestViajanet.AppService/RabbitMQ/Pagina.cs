namespace MyTestViajanet.AppService.RabbitMQ
{
    using System;
    using MyTestViajanet.AppService.Interfaces;
    using MyTestViajaNet.DomainService.Entities;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    public class Pagina
    {
        private SeleniumConfigurations _configurations;
        private IWebDriver _driver;

        private readonly IBatePapoOnlineAppService batePapoOnlineAppService;

        public Pagina(SeleniumConfigurations seleniumConfigurations)
        {
            _configurations = seleniumConfigurations;
            FirefoxOptions optionsFF = new FirefoxOptions();
            optionsFF.AddArgument("--headless");

            _driver = new FirefoxDriver(
                _configurations.CaminhoDriverFirefox
                , optionsFF);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(30);
            _driver.Navigate().GoToUrl(
                _configurations.UrlPaginaCotacoes);
        }

        public BatePapoOnline ObterDados()
        {

            var batePapoOnline = new BatePapoOnline();


            var pessoasOnline = _driver.FindElement(By.ClassName("stats-users-online"))
                .FindElement(By.TagName("span")).Text;

            var lugaresDisponiveis = _driver.FindElement(By.ClassName("stats-rooms-avaliable"))
              .FindElement(By.TagName("span")).Text;


            batePapoOnline.PessoasOnline = pessoasOnline;
            batePapoOnline.LugaresDisponiveis = lugaresDisponiveis;
            batePapoOnline.DataCadastro = DateTime.Now;
            batePapoOnline.DataRegistro = DateTime.Now;

            
            //batePapoOnline.LugaresDisponiveis = dados.FindElement(By.TagName("tbody"));

            return batePapoOnline;
        }

        public void GravarDados()
        {
           var dados = ObterDados();

            batePapoOnlineAppService.Add(dados);
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}

