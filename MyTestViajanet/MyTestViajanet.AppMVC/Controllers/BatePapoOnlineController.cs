using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTestViajanet.AppService.Interfaces;
using MyTestViajanet.AppService.ViewModel;
using MyTestViajaNet.DomainService.Entities;
using RabbitMQ.Client;

namespace MyTestViajanet.AppMVC.Controllers
{
    public class BatePapoOnlineController : Controller
    {
        private readonly IBatePapoOnlineAppService batePapoOnlineAppService;
        public BatePapoOnlineController(IBatePapoOnlineAppService batePapoOnlineAppService)
        {
            this.batePapoOnlineAppService = batePapoOnlineAppService;
        }
        // GET: BatePapoOnline
        public ActionResult Index()
        {
            var batePapoOnline = batePapoOnlineAppService.GetAll();

            var batePapoOnlineViewModel = Mapper.Map<IEnumerable<BatePapoOnline>, IEnumerable<BatePapoOnlineViewModel>>(batePapoOnline);
            return View(batePapoOnlineViewModel);
        }

        [HttpGet]
        public ActionResult Carregar(
         [FromServices]RabbitMQConfigurations rabbitMQConfigurations)
        {
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

                string message = "Solicitação de Carga - " +
                    $"API Bate Papo - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "Carregar",
                                     basicProperties: null,
                                     body: body);
            }

            return RedirectToAction("Index");
        }
    }
}