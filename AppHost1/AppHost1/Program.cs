var builder = DistributedApplication.CreateBuilder(args);

var serviceBus = builder.AddAzureServiceBus("service-bus-test")
    .RunAsEmulator(options =>
    {
        options.WithHostPort(5672);
    });

serviceBus.AddServiceBusQueue("queue");

var username = builder.AddParameter("username", secret: true, value: "postgres");
var password = builder.AddParameter("password", secret: true, value: "postgres");
var postgres = builder.AddPostgres("server", username, password);
if (builder.Environment.EnvironmentName == "Development")
{
    postgres.WithDataVolume();
}

postgres.AddDatabase("database");


builder.Build().Run();
