using Confluent.Kafka;
using Microservice.Common.Interfaces;
using Microservice.Common.Models;
using Newtonsoft.Json;

namespace Microservice.Common.MessageBrokers;

public class KafkaMessageBroker : IMessageBroker
{
    private readonly IProducer<Null, string> _producer;
    private readonly IConsumer<Null, string> _consumer;
    private readonly string _consumerTopic;

    public KafkaMessageBroker(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public KafkaMessageBroker(IConsumer<Null, string> consumer, string topic)
    {
        _consumer = consumer;
        _consumerTopic = topic;
        _consumer.Subscribe(topic);
    }

    public void Publish(string topic, Frame frame)
    {
        var message = JsonConvert.SerializeObject(frame);
        _producer.Produce(topic, new Message<Null, string> { Value = message });
    }

    public Frame Consume(string topic)
    {
        var consumeResult = _consumer.Consume();
        return JsonConvert.DeserializeObject<Frame>(consumeResult.Message.Value);
    }
}