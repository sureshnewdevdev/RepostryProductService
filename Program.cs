using Consul;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Register ProductService with Consul
var consulClient = new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500");
});

var registration = new AgentServiceRegistration()
{
    ID = "ProductService",
    Name = "ProductService",
    Address = "localhost",
    Port = 5002
};

consulClient.Agent.ServiceRegister(registration).Wait();

app.MapGet("/api/product", () => "Product service is working");

app.Run();
