
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using EventManagement.Application.Behaviors;

namespace EventManagement.Application {
    public static class ServicesExtensions {
        public static IServiceCollection AddApplication(this IServiceCollection services) {          
            _ = services.AddMediatR(AssemblyReference.Assembly);
            _ = services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            _ = services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
            return services;
        }        
    }
}
