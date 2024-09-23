using OpenCvSharp;
using System.Drawing;

namespace Cam.Con.Strategy;

// Context used by strategt
public class ImageProcessor
{
    private IImageStrategy _strategy;

    public ImageProcessor(IImageStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IImageStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ProcessImage(object imageData)
    {
        _strategy.ProcessImage(imageData);
    }

    public object ResizeImage(object imageData, int width, int height)
    {
        return _strategy.Resize(imageData, width, height);
    }
}

// Factory to create the strategy
public static class ImageStrategyFactory
{
    public static IImageStrategy CreateStrategy(object imageData)
    {
        if (imageData is Mat)
        {
            return new MatStrategy();
        }
        else if (imageData is Bitmap)
        {
            return new BitmapStrategy();
        }
        else
        {
            throw new ArgumentException("Tipo de imagen no soportado");
        }
    }
}