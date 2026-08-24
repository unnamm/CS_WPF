using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Starter
{
    internal class ViewContainer
    {
        readonly Dictionary<Type, Type> viewPair = [];
        readonly IServiceCollection _service;

        public ViewContainer(IServiceCollection service)
        {
            _service = service;
        }

        public ViewContainer AddViewViewModel<T, K>() where T : ContentControl where K : class
        {
            _service.AddSingleton<T>();
            _service.AddSingleton<K>();
            viewPair.Add(typeof(T), typeof(K));

            return this;
        }

        public void ConnectContext(IServiceProvider provider)
        {
            foreach (var pair in viewPair)
            {
                ContentControl view = (ContentControl)provider.GetRequiredService(pair.Key);
                var viewmodel = provider.GetRequiredService(pair.Value);
                view.DataContext = viewmodel;
            }
        }
    }
}
