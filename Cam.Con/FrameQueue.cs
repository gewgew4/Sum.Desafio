namespace Cam.Con;

using Cam.Con.Strategy;
using OpenCvSharp;
using System;
using System.Collections.Concurrent;

public class FrameQueue
{
    private readonly BlockingCollection<object> _frameQueue;
    private readonly ImageProcessor _imageProcessor;

    public FrameQueue(int maxCapacity = 100)
    {
        _frameQueue = new BlockingCollection<object>(new ConcurrentQueue<object>(), maxCapacity);
        // Default strategy
        _imageProcessor = new ImageProcessor(new MatStrategy());
    }

    public void PushQueue(object frame)
    {
        _frameQueue.Add(frame);
        Console.WriteLine("Frame pushed.");
    }

    public object PullQueue()
    {
        var frame = _frameQueue.Take();
        Console.WriteLine("Frame pulled.");

        return frame;
    }

    public int GetQueueCount()
    {
        return _frameQueue.Count;
    }

    public void CompleteAdding()
    {
        _frameQueue.CompleteAdding();
    }

    public void ClearQueue()
    {
        while (_frameQueue.TryTake(out object frame))
        {
            DisposeFrame(frame);
        }
        Console.WriteLine("Queue cleared");
    }

    private void DisposeFrame(object frame)
    {
        if (frame is IDisposable disposable)
        {
            disposable.Dispose();
        }
        else if (frame is Mat mat)
        {
            mat.Dispose();
        }
        // HACK: remember to check for other types of images(strategies) and dispose them accordingly
    }

    public void ProcessFrame(object frame)
    {
        var strategy = ImageStrategyFactory.CreateStrategy(frame);
        _imageProcessor.SetStrategy(strategy);
        _imageProcessor.ProcessImage(frame);

        // Resize operation
        object resizedFrame = _imageProcessor.ResizeImage(frame, 640, 480);

        DisposeFrame(resizedFrame);
    }
}
