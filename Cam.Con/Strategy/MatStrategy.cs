using OpenCvSharp;

namespace Cam.Con.Strategy;

// Concrete strategy for Mat
public class MatStrategy : IImageStrategy
{
    public void ProcessImage(object imageData)
    {
        var mat = imageData as Mat;

        // Here we can process the image, if necessary
        Console.WriteLine("Proccesing Mat");
    }

    public object Resize(object imageData, int width, int height)
    {
        var mat = imageData as Mat;
        var resized = new Mat();
        Cv2.Resize(mat, resized, new Size(width, height));

        return resized;
    }
}
