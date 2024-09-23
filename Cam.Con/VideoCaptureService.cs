using OpenCvSharp;

namespace Cam.Con;

public class VideoCaptureService
{
    private readonly FrameQueue _frameQueue;
    private readonly VideoCapture _capture;
    private Thread _captureThread;
    private bool _isCapturing;

    public VideoCaptureService(string source, FrameQueue frameQueue)
    {
        _frameQueue = frameQueue;
        _capture = new VideoCapture();

        // Open camera by index or file path
        if (int.TryParse(source, out int cameraIndex))
        {
            // Webcam
            _capture.Open(cameraIndex);
        }
        else
        {
            // File path
            _capture.Open(source);
        }

        if (!_capture.IsOpened())
        {
            throw new Exception("Can't open webcam.");
        }

        _isCapturing = false;
    }

    public void StartCapture()
    {
        if (_isCapturing)
            throw new InvalidOperationException("Capturing.");

        _isCapturing = true;

        _captureThread = new Thread(() =>
        {
            while (_isCapturing)
            {
                var frame = new Mat();
                _capture.Read(frame);

                if (frame.Empty()) continue;

                _frameQueue.PushQueue(frame);
            }
        });

        _captureThread.Start();
    }

    public void StopCapture()
    {
        _isCapturing = false;
        _captureThread?.Join();
        _capture.Release();
    }
}
