namespace MyTestViajanet.RabbitMQ
{
    using global::RabbitMQ.Client;
    using global::RabbitMQ.Client.Events;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MyTestViajanet.AppService;
    using MyTestViajanet.AppService.Interfaces;
    using MyTestViajanet.AppService.RabbitMQ;
    using MyTestViajanet.Infra.Data.Repositories;
    using MyTestViajaNet.DomainService.Entities;
    using MyTestViajaNet.DomainService.Interfaces.Repositories;
    using MyTestViajaNet.DomainService.Interfaces.Services;
    using MyTestViajaNet.DomainService.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class Program
    {
        private static IConfiguration _configuration;
        private static SeleniumConfigurations _seleniumConfigurations;

        public static object IbatePapoOnlineAppService { get; private set; }

        static void Main(string[] args)
        {

 

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();

            var rabbitMQConfigurations = new RabbitMQConfigurations();
            new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
                _configuration.GetSection("RabbitMQConfigurations"))
                    .Configure(rabbitMQConfigurations);


            _seleniumConfigurations = new SeleniumConfigurations();
            new ConfigureFromConfigurationOptions<SeleniumConfigurations>(
                _configuration.GetSection("SeleniumConfigurations"))
                    .Configure(_seleniumConfigurations);

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfigurations.HostName,
                Port = rabbitMQConfigurations.Port,
                UserName = rabbitMQConfigurations.UserName,
                Password = rabbitMQConfigurations.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Carregar",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue: "Carregar",
                     autoAck: true,
                     consumer: consumer);

                Console.WriteLine("Aguardando mensagens para processamento");
                Console.WriteLine("Pressione uma tecla para encerrar...");
                Console.ReadKey();

            }
        }

        private static void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app

            var message = Encoding.UTF8.GetString(e.Body);
            Console.WriteLine(Environment.NewLine +
                "[Nova mensagem recebida] " + message);


            Pagina pagina =
                new Pagina(_seleniumConfigurations);
            try
            {
                Console.WriteLine("Iniciando extração dos dados...");
                pagina.CarregarPagina();

                Console.WriteLine("Dados extraídos com sucesso!");
                var teste = pagina.ObterDados();
                serviceProvider.GetService<IBatePapoOnlineAppService>().Add(teste);
                Console.WriteLine("Carga dos dados efetuada com sucesso!");
            }
            finally
            {
                pagina.Fechar();
            }
        }


        public static void ConfigureServices(IServiceCollection services)
        {
            Configure(services, MyTestViajanet.Infra.Data.Module.GetTypes());
            Configure(services, MyTestViajaNet.DomainService.IoC.Module.GetTypes());
            Configure(services, MyTestViajaNet.AppServices.IoC.Module.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}
