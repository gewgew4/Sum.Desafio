using Microservice.Common.Interfaces;
using Microservice.Common.Models;
using OpenCvSharp;

namespace Microservice.ServiceCapture;

public class CaptureService
{
    private readonly IMessageBroker _messageBroker;
    private readonly VideoCapture _capture;
    private readonly IFrameStrategy _frameStrategy;

    public CaptureService(IMessageBroker messageBroker, IFrameStrategy frameStrategy)
    {
        _messageBroker = messageBroker;
        _frameStrategy = frameStrategy;
        _capture = new VideoCapture(0);
    }

    public void Start()
    {
        while (true)
        {
            using (var mat = new Mat())
            {
                _capture.Read(mat);
                if (mat.Empty()) continue;

                var frame = new Frame
                {
                    Data = _frameStrategy.Serialize(mat),
                    StrategyType = _frameStrategy.GetType().Name,
                    Width = mat.Width,
                    Height = mat.Height
                };

                _messageBroker.Publish("raw_frames", frame);
            }
        }
    }
}
