using Microservice.Common.Interfaces;
using Microservice.Common.Models;

namespace Microservice.ServiceProcessing;

public class ProcessingService
{
    private readonly IMessageBroker _consumerBroker;
    private readonly IMessageBroker _producerBroker;
    private readonly Dictionary<string, IFrameStrategy> _strategies;

    public ProcessingService(IMessageBroker consumerBroker, IMessageBroker producerBroker, IEnumerable<IFrameStrategy> strategies)
    {
        _consumerBroker = consumerBroker;
        _producerBroker = producerBroker;
        _strategies = strategies.ToDictionary(s => s.GetType().Name);
    }

    public void Start()
    {
        while (true)
        {
            var frame = _consumerBroker.Consume("frames_to_process");
            if (!_strategies.TryGetValue(frame.StrategyType, out var strategy))
            {
                Console.WriteLine($"Strategy not found: {frame.StrategyType}");
                continue;
            }

            var deserializedFrame = strategy.Deserialize(frame.Data);
            var resizedFrame = strategy.Resize(deserializedFrame, 640, 480);
            var serializedResizedFrame = strategy.Serialize(resizedFrame);

            var processedFrame = new Frame
            {
                Data = serializedResizedFrame,
                StrategyType = frame.StrategyType,
                Width = 640,
                Height = 480
            };

            _producerBroker.Publish("processed_frames", processedFrame);
        }
    }
}