using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Starter
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// regist View and ViewModel, auto connect datacontext
        /// </summary>
        public static IServiceCollection AddSingletonViewModel<TView, TViewModel>(this IServiceCollection services)
            where TView : FrameworkElement where TViewModel : class
        {
            services.AddSingleton<TViewModel>();
            services.AddSingleton(sp =>
            {
                var view = ActivatorUtilities.CreateInstance<TView>(sp);
                view.DataContext = sp.GetRequiredService<TViewModel>();
                return view;
            });
            return services;
        }
    }
}
