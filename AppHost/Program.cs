var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();

var cosmos = builder.AddAzureCosmosDB("cosmos-db")
    .RunAsEmulator();


var serviceBus = builder.AddAzureServiceBus("serviceBus")
    .RunAsEmulator();

serviceBus.AddServiceBusQueue(name: "my-first-queue");
serviceBus.AddServiceBusTopic(name: "my-first-topic");
serviceBus.AddSubscription("my-first-topic", "sub-name");


var cache = builder.AddRedis("cache")
    .WithRedisInsight()
    .WithRedisCommander();

var functions = builder.AddAzureFunctionsProject<Projects.FunctionApp>("functionApp")
    .WithHostStorage(storage)
    .WithReference(serviceBus)
    .WaitFor(serviceBus)
    .WithExternalHttpEndpoints();
    
builder.Build().Run();    