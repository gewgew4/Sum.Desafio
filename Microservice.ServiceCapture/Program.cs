namespace Microservice.ServiceCapture;

using Confluent.Kafka;
using Microservice.Common.Interfaces;
using Microservice.Common.MessageBrokers;
using Microservice.Common.Strategies;
using System.Runtime.InteropServices;

class Program
{
    static Program()
    {
        string libraryName = "libOpenCvSharpExtern.so";
        string libraryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, libraryName);

        Console.WriteLine($"Loading libraries from: {libraryPath}");

        if (File.Exists(libraryPath))
        {
            try
            {
                NativeLibrary.Load(libraryPath);
                Console.WriteLine("Library loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        else
        {
            Console.WriteLine("File not found.");
            throw new FileNotFoundException($"Library not found: {libraryName}");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Starting Microservice.ServiceCapture...");

        var config = new ProducerConfig
        {
            BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS")
        };
        Console.WriteLine($"Kafka Bootstrap Servers: {config.BootstrapServers}");

        using var producer = new ProducerBuilder<Null, string>(config).Build();
        IFrameStrategy frameStrategy = new MatFrameStrategy();
        var captureService = new CaptureService(new KafkaMessageBroker(producer), frameStrategy);

        captureService.Start();
    }
}