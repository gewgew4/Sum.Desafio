using Confluent.Kafka;
using Microservice.Common.Interfaces;
using Microservice.Common.MessageBrokers;
using Microservice.Common.Strategies;

namespace Microservice.ServiceProcessing;

class Program
{
    static void Main(string[] args)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS"),
            GroupId = "processing-service-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        var producerConfig = new ProducerConfig { BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") };

        using var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

        var strategies = new IFrameStrategy[] { new MatFrameStrategy() };
        var processingService = new ProcessingService(
            new KafkaMessageBroker(consumer, "frames_to_process"),
            new KafkaMessageBroker(producer),
            strategies
        );
        processingService.Start();
    }
}
