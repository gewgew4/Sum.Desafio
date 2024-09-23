using Confluent.Kafka;
using Microservice.Common.MessageBrokers;

namespace Microservice.ServiceQueue;

class Program
{
    static void Main(string[] args)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS"),
            GroupId = "queue-service-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        var producerConfig = new ProducerConfig { BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") };

        using var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

        var queueService = new QueueService(
            new KafkaMessageBroker(consumer, "raw_frames"),
            new KafkaMessageBroker(producer)
        );
        queueService.Start();
    }
}