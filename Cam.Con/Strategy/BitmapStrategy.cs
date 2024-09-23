using System.Drawing;

namespace Cam.Con.Strategy;

// Concrete strategy for Bitmap
public class BitmapStrategy : IImageStrategy
{
    public void ProcessImage(object imageData)
    {
        var bitmap = imageData as Bitmap;

        // Here we can process the image, if necessary
        Console.WriteLine("Proccesing Bitmap");
    }

    public object Resize(object imageData, int width, int height)
    {
        var bitmap = imageData as Bitmap;

        return new Bitmap(bitmap, new Size(width, height));
    }
}