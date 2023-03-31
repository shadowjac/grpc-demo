using gRpcDemo.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();


var app = builder.Build();

//https://learn.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-7.0
/*builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(7065, o => o.Protocols = HttpProtocols.Http2);
});*/

// Configure the HTTP request pipeline.
app.MapGrpcService <DriverService>();
app.MapGrpcService <ChatService>();

//https://learn.microsoft.com/en-us/aspnet/core/grpc/test-tools?view=aspnetcore-7.0
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
