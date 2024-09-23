using Cam.Con;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Cam.WF;

public partial class Form1 : Form
{
    private FrameQueue _frameQueue;
    private VideoCaptureService _videoCaptureService;
    private Task _producerTask;
    private Task _consumerTask;
    private CancellationTokenSource _cancellationTokenSource;
    private string _selectedVideoPath;

    public Form1()
    {
        InitializeComponent();

        // Limit the queue to 50 frames
        _frameQueue = new FrameQueue(50);

        _selectedVideoPath = string.Empty;

        comboBoxSource.Items.Add("Webcam (Default)");
        comboBoxSource.SelectedIndex = 0;
        buttonStop.Enabled = false;
    }

    private void buttonSelect_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        openFileDialog.Filter = "Video files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All files (*.*)|*.*";
        openFileDialog.FilterIndex = 1;
        openFileDialog.RestoreDirectory = true;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _selectedVideoPath = openFileDialog.FileName;
            comboBoxSource.Items.Clear();
            comboBoxSource.Items.Add("Webcam (Default)");
            comboBoxSource.Items.Add(_selectedVideoPath);
            comboBoxSource.SelectedIndex = 1;
        }
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        buttonStart.Enabled = false;
        buttonStop.Enabled = true;
        pictureBoxVideo.Show();

        string source;

        if (comboBoxSource.SelectedIndex == 0)
        {
            // Webcam by default
            source = "0";
        }
        else
        {
            source = _selectedVideoPath;
        }

        if (string.IsNullOrEmpty(source))
        {
            MessageBox.Show("Invalid source.");
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            return;
        }

        _videoCaptureService = new VideoCaptureService(source, _frameQueue);

        // Create a CancellationTokenSource to cancel the tasks
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        _producerTask = Task.Run(() => _videoCaptureService.StartCapture(), token);
        _consumerTask = Task.Run(() => DisplayFrames(token), token);
    }

    private void buttonStop_Click(object sender, EventArgs e)
    {
        buttonStart.Enabled = true;
        buttonStop.Enabled = false;

        _cancellationTokenSource?.Cancel();
        _videoCaptureService?.StopCapture();

        try
        {
            // Wait for the tasks to finish
            Task.WaitAll(new[] { _producerTask, _consumerTask }, TimeSpan.FromSeconds(2));
        }
        catch (AggregateException)
        {
            // Ignore task cancellation exceptions
        }

        _frameQueue.ClearQueue();
        pictureBoxVideo.Image = null;
        pictureBoxVideo.Hide();
    }

    private void DisplayFrames(CancellationToken token)
    {
        while (!token.IsCancellationRequested || _frameQueue.GetQueueCount() > 0)
        {
            if (token.IsCancellationRequested && _frameQueue.GetQueueCount() == 0)
            {
                // If cancellation is requested and there are no more frames, exit the loop
                break;
            }

            try
            {
                object frame = _frameQueue.PullQueue();
                if (frame is Mat matFrame)
                {
                    var resizedMat = new Mat();
                    Cv2.Resize(matFrame, resizedMat, new OpenCvSharp.Size(640, 480));

                    var bitmap = BitmapConverter.ToBitmap(resizedMat);

                    Invoke((MethodInvoker)delegate
                    {
                        pictureBoxVideo.Image?.Dispose();
                        pictureBoxVideo.Image = bitmap;
                    });

                    resizedMat.Dispose();
                    matFrame.Dispose();
                }
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _videoCaptureService?.StopCapture();

        try
        {
            Task.WaitAll(new[] { _producerTask, _consumerTask }, TimeSpan.FromSeconds(5));
        }
        catch (AggregateException)
        {
            // Ignore task cancellation exceptions
        }

        _frameQueue.ClearQueue();
    }
}

