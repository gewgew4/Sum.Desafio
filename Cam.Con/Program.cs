namespace Cam.Con;

using OpenCvSharp;
using System;
using System.Threading;

class Program
{
    // Limit frames' quantity
    private static FrameQueue _frameQueue = new FrameQueue(50);
    private static VideoCaptureService _videoCaptureService;
    private static Thread _producerThread;
    private static Thread _consumerThread;
    private static bool _isProcessing;

    static void Main(string[] args)
    {
        Console.WriteLine("Starting");

        var source = "0";

        _videoCaptureService = new VideoCaptureService(source, _frameQueue);

        // Start (ProducerThread)
        _producerThread = new Thread(() => _videoCaptureService.StartCapture());
        _producerThread.Start();

        // Start (ConsumerThread)
        _isProcessing = true;
        _consumerThread = new Thread(ProcessFrames);
        _consumerThread.Start();

        Console.WriteLine("Capture and proccesing started. Press any key to stop...");
        Console.ReadKey();

        // Stops the capture and processing
        _videoCaptureService.StopCapture();
        _isProcessing = false;

        // Wait for the threads to finish
        _producerThread.Join(); 
        _consumerThread.Join();

        _frameQueue.CompleteAdding();
        _frameQueue.ClearQueue();

        Console.WriteLine("End");
    }

    // Process the frames in the queue
    private static void ProcessFrames()
    {
        while (_isProcessing || _frameQueue.GetQueueCount() > 0)
        {
            try
            {
                object frame = _frameQueue.PullQueue();
                _frameQueue.ProcessFrame(frame);
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }

        _frameQueue.ClearQueue();
    }
}
