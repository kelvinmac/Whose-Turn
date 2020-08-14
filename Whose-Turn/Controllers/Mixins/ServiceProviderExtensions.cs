using Microsoft.Extensions.DependencyInjection;

namespace Whose_Turn.Controllers.Mixins
{
    public static class ServiceProviderExtensions
    {
        public static void AddControllerMixins(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<HouseholdMixins>();
            serviceCollection.AddScoped<TodosMixins>();
        }
    }
}