using Grpc.Net.Client;
using gRpcDemo.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5062");
var client = new gRpcDemo.Client.DriverService.DriverServiceClient(channel);

var drivers = client.GetDrivers(new());

foreach(var driver in drivers.Drivers)
{
    Console.WriteLine($"Driver {driver.Name} - N°: {driver.CarNumber}");
}
