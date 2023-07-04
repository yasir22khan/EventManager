using EventManagement.App.Configuration;
using EventManagement.App.DataSeeder;
using EventManagement.Domain.Repositories;
using EventManagement.Persistence.Repositories;
using Scrutor;

using System.Text.Json.Serialization;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                EventManagement.Infrastructure.AssemblyReference.Assembly,
                EventManagement.Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.AddMemoryCache();
builder.Services.AddApplication();
builder.Services.AddTransient<DataSeeder>();

//setup infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

//db setup
builder.Services.AddPersistence(builder.Configuration);

builder
    .Services
    .AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }
    )
    .AddApplicationPart(EventManagement.Presentation.AssemblyReference.Assembly);
builder.Services.AddSwaggerGen();
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

if (args.Length == 1 && args[0].ToLower() == "seeddata") {
    Console.WriteLine("inside seeding");
    SeedData(app);
}

//Seed Data
static void SeedData(IHost app) {
    var dataSeeder = app.Services.GetService<IServiceScopeFactory>()
    .CreateScope()
    .ServiceProvider
    .GetService<DataSeeder>();
    dataSeeder.Seed();
}

app.UseHttpsRedirection();
app.UseCors(Common.DefaultCorsPolicy);
app.UseAuthorization();
app.MapControllers();
app.Run();