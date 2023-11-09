using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;

namespace Consumer
{
    public class KafkaConsumerConfig
    {
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumerConfig(IConfiguration configuration)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"],
                GroupId = "test",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false, // default: true. Trường hợp, tắt auto commit để kiểm soát commit offset thủ công
                EnableAutoOffsetStore = false
            };

            _consumer = new ConsumerBuilder<Null, string>(config)
                            .SetErrorHandler((_, e) => HandleError(e))
                            .Build();
        }

        public string ConsumerMessage()
        {
            var resultMessage = string.Empty;
            var consumeResult = _consumer.Consume();

            if (consumeResult.Message != null)
            {
                // Xử lý thông điệp thành công
                Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");

                // Thực hiện commit offset
                _consumer.Commit(consumeResult);

                //string resultDeserialize = JsonConvert.DeserializeObject<string>(consumeResult.Message.Value);
                //return resultDeserialize;
                resultMessage = consumeResult.Message.Value;
            }
            else
            {
                // Xử lý lỗi và gửi thông điệp vào dead letter topic
                Console.WriteLine("Handle error sent message to dead letter");
                SendToDeadLetterTopic(consumeResult.Message);
            }

            return resultMessage;
        }

        private void HandleError(Error error)
        {
            // Xử lý lỗi toàn cục cho consumer
            Console.WriteLine($"Global error handler: {error.Reason}");
        }

        public void Subscribe(string topic)
        {
            _consumer?.Subscribe(topic);
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }

        private void SendToDeadLetterTopic(Message<Null, string> message)
        {
            // Thực hiện logic để gửi thông điệp vào dead letter topic
            // Sử dụng một producer để gửi thông điệp đến dead letter topic
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };

            using (var producer = new ProducerBuilder<Ignore, string>(producerConfig).Build())
            {
                var deadLetterTopic = "dead-event";

                var deadLetterMessage = new Message<Ignore, string>
                {
                    Value = message.Value,
                    Headers = message.Headers // Bạn cũng có thể chuyển các header nếu cần thiết
                };

                producer.ProduceAsync(deadLetterTopic, deadLetterMessage);
            }
        }
    }
}
