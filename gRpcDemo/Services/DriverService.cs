using System;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRpcDemo.Data;

namespace gRpcDemo.Services
{
    // https://visualstudiomagazine.com/articles/2020/01/16/grpc-well-known-types.aspx
    public class DriverService : gRpcDemo.DriverService.DriverServiceBase
	{
        private readonly ILogger<DriverService> _logger;

        public DriverService(ILogger<DriverService> logger)
        {
            _logger = logger;
        }

        public override Task<MultiDriverResponse> GetDrivers(Empty request, ServerCallContext context)
        {

            var response = new MultiDriverResponse();
            response.Drivers.AddRange(Drivers.ListDrivers);

            return Task.FromResult(response);
        }

        public override async Task GetDriversStream(
            Query request,
            IServerStreamWriter<DriverResponse> responseStream,
            ServerCallContext context)
        {
            for (int i = 0; i < 30; i++)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Request was cancelled");
                    break;
                }

                var response = Drivers.ListDrivers.Where(p => p.CarNumber == request.CarNumber).SingleOrDefault();
                await responseStream.WriteAsync(response!);
                await Task.Delay(1000);
            }
        }

        public override async Task<MultiDriverResponse> RegisterLapStream(
            IAsyncStreamReader<TimePerLapRequest> requestStream,
            ServerCallContext context)
        {
            var response = new MultiDriverResponse
            {
                Drivers = { }
            };

            await foreach (var request in requestStream.ReadAllAsync())
            {
                response.Drivers.Add(new DriverResponse
                {
                    Name = "Lewis Hamilton",
                    CarNumber = 44,
                    Experience = Experience.Veteran,
                    Team = "Mercedes",
                    BirthDate = Timestamp.FromDateTime(new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc)),
                    Timestamp = Timestamp.FromDateTime(DateTime.UtcNow),
                    LastLapTime = request.LapTime
                });
            }
            return response;
        }

        public override async Task<Empty> RegisterLapNoResponseStream(
            IAsyncStreamReader<TimePerLapRequest> requestStream,
            ServerCallContext context)
        {
            var response = new MultiDriverResponse
            {
                Drivers = { }
            };

            await foreach (var request in requestStream.ReadAllAsync())
            {
                _logger.LogInformation("Number: {Driver} - Lap: {LapTime}", request.Driver, request.LapTime);
            }
            return new();
        }

    }
}

