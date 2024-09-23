namespace Microservice.Common.Strategies;

using Microservice.Common.Interfaces;
using OpenCvSharp;

public class MatFrameStrategy : IFrameStrategy
{
    public byte[] Serialize(object frame)
    {
        var mat = frame as Mat;
        return mat?.ToBytes();
    }

    public object Deserialize(byte[] data)
    {
        return Mat.FromImageData(data);
    }

    public object Resize(object frame, int width, int height)
    {
        var mat = frame as Mat;
        var resized = new Mat();
        Cv2.Resize(mat, resized, new OpenCvSharp.Size(width, height));
        return resized;
    }
}