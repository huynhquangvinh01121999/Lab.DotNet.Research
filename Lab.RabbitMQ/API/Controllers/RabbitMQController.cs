using API.RabitMQ;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IRabitMQProducer _rabitMQProducer;

        public RabbitMQController(IRabitMQProducer rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpPost("create-exchange")]
        public IActionResult Post(string exchangeName)
        {

            _rabitMQProducer.CreateExchange(exchangeName);

            return Ok();
        }

        [HttpPost("create-queue")]
        public IActionResult Post(string queueName, string exchangeName)
        {
            
            _rabitMQProducer.CreateQueue(queueName, exchangeName);

            return Ok();
        }

        [HttpPost("send-message")]
        public IActionResult Post_SendMessage(string queueName, string exchangeName)
        {
            var rng = new Random();
            var weathers = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            _rabitMQProducer.SendMessage(weathers, queueName, exchangeName);

            return Ok();
        }
    }
}
