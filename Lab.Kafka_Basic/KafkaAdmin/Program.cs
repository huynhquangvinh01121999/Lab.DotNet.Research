using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
using System.Collections.Generic;

namespace KafkaAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var adminClientConfig = new AdminClientConfig
            {
                BootstrapServers = "localhost:9092", // Địa chỉ Kafka Broker
            };

            using (var adminClient = new AdminClientBuilder(adminClientConfig).Build())
            {
                var listTopic = new List<TopicSpecification>();
                listTopic.Add(new TopicSpecification
                {
                    Name = "topic-1",
                    NumPartitions = 3,  // số lượng partition
                    ReplicationFactor = 1   // số bản sao chép của mỗi partition - gồm 1 leader & (replicationFactor - 1) follower)
                });

                try
                {
                    adminClient.CreateTopicsAsync(listTopic).Wait();
                    Console.WriteLine("Topic created successfully.");
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occurred: {e.Results[0].Error.Reason}");
                }
            }
        }
    }
}
