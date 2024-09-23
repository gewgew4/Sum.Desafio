using Microservice.Common.Interfaces;
using Microservice.Common.Models;
using System.Collections.Concurrent;

namespace Microservice.ServiceQueue;

public class QueueService
{
    private readonly IMessageBroker _consumerBroker;
    private readonly IMessageBroker _producerBroker;
    private readonly BlockingCollection<Frame> _queue;

    public QueueService(IMessageBroker consumerBroker, IMessageBroker producerBroker)
    {
        _consumerBroker = consumerBroker;
        _producerBroker = producerBroker;
        _queue = new BlockingCollection<Frame>(new ConcurrentQueue<Frame>(), 100);
    }

    public void Start()
    {
        Task.Run(() => ConsumeRawFrames());
        Task.Run(() => PublishFramesForProcessing());
    }

    private void ConsumeRawFrames()
    {
        while (true)
        {
            var frame = _consumerBroker.Consume("raw_frames");
            _queue.Add(frame);
        }
    }

    private void PublishFramesForProcessing()
    {
        foreach (var frame in _queue.GetConsumingEnumerable())
        {
            _producerBroker.Publish("frames_to_process", frame);
        }
    }
}