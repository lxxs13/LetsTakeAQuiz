using GeekQuiz.Interfaces;
using GeekQuiz.Models;
using GeekQuiz.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGeekQuizServices(this IServiceCollection services)
        {
            services.AddDbContext<GeekQuizContext>();
            services.AddTransient<IQuestion, QuestionsService>();
            services.AddTransient<ICategory, CategoriesService>();
            services.AddTransient<IPlayer, PlayerService>();

            return services;
        }
    }
}
