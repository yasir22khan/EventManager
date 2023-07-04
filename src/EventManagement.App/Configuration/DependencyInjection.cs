using EventManagement.Application.Behaviors;
using EventManagement.Domain.Repositories;
using EventManagement.Persistence;
using EventManagement.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scrutor;
using System.Text.Json.Serialization;

namespace EventManagement.App.Configuration {
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration) {
            services
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            EventManagement.Infrastructure.AssemblyReference.Assembly,
                            EventManagement.Persistence.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime())
                .AddCustomCors(configuration);
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddMediatR(EventManagement.Application.AssemblyReference.Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

          
            services.AddValidatorsFromAssembly(EventManagement.Application.AssemblyReference.Assembly,
                includeInternalTypes: true);

            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services) {
            services
                .AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                })
                .AddApplicationPart(EventManagement.Presentation.AssemblyReference.Assembly);
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection
            AddPersistence(this IServiceCollection services, IConfiguration configuration) {
            string connectionString = configuration["Data:Database"];
            services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) => {
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                        .UseSnakeCaseNamingConvention();
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();

            return services;
        }

        private static IServiceCollection
            AddCustomCors(this IServiceCollection services, IConfiguration configuration) {
            services.AddCors(options => {
                options.AddPolicy(Common.DefaultCorsPolicy,
                    builder => {
                        var corsList = configuration.GetSection("CorsOrigins").Get<List<string>>()?.ToArray() ??
                                       Array.Empty<string>();
                        builder.WithOrigins(corsList)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin();
                    });
            });

            return services;
        }
    }
}