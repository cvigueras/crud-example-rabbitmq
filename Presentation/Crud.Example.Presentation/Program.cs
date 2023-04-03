using Crud.Example.Api;

//Creating builder.
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//Init startup constructor.
var start = new Startup(configuration);

//Configuring services.
start.ConfigureServices(builder);

//Building.
var app = builder.Build();

//Configure app.
start.Configure(app);