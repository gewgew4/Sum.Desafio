using Microservice.Common.Models;

namespace Microservice.Common.Interfaces;

public interface IMessageBroker
{
    void Publish(string topic, Frame frame);
    Frame Consume(string topic);
}
