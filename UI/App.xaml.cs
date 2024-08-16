using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using UI.DataGridManage;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var build = Host.CreateApplicationBuilder();

            build.Services.AddTransient<DataGridView>();
            build.Services.AddTransient<DataGridViewModel>();

            var host = build.Build();
        }
    }
}
