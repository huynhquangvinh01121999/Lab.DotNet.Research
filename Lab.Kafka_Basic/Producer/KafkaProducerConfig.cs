using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Producer
{
    public class KafkaProducerConfig
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerConfig(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"],
                //MessageSendMaxRetries = 3, // Số lần thử lại khi gửi tin nhắn thất bại,
                //MessageTimeoutMs = 100 // Thời gian timeout khi gửi message
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceMessage(string topic, string message)
        {
            // gửi message vào partition chỉ định trong topic
            // lưu ý: partition đc chỉ định nếu # 0 thì phải đc tạo trước đó (tức topic nào có 2 partition trở lên)
            var topicPart = new TopicPartition(topic, new Partition(0));
            string serialized = JsonConvert.SerializeObject(message);
            await _producer.ProduceAsync(topicPart, new Message<Null, string> { Value = serialized });
            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}
