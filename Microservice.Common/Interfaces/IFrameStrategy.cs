namespace Microservice.Common.Interfaces;

public interface IFrameStrategy
{
    byte[] Serialize(object frame);
    object Deserialize(byte[] data);
    object Resize(object frame, int width, int height);
}