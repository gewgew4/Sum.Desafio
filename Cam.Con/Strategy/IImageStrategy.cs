namespace Cam.Con.Strategy;

public interface IImageStrategy
{
    void ProcessImage(object imageData);
    object Resize(object imageData, int width, int height);
}