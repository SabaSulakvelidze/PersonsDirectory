using Microsoft.Extensions.DependencyInjection;
using PersonsDirectory.Application.Common.Behaviors;
using PersonsDirectory.Application.Common.Validation;
using PersonsDirectory.Application.Persons.Commands.CreatePerson;
using PersonsDirectory.Application.Persons.Commands.UpdatePerson;
using PersonsDirectory.Application.Persons.Validation;
using System.Reflection;

namespace PersonsDirectory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonValidator>();
            services.AddScoped<IValidator<UpdatePersonCommand>, UpdatePersonValidator>();


            return services;
        }
    }
}
