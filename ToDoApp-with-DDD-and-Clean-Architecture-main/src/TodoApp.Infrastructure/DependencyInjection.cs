using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Repositories;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, EfCoreUserRepository>();
            services.AddScoped<ITodoListRepository, EfCoreTodoListRepository>();

            return services;
        }
    }
}
