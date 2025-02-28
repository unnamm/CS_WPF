using Common.Message;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModel;

namespace UI.View
{
    /// <summary>
    /// DialogView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DialogView : UserControl, IRecipient<DialogMessage>
    {
        public DialogView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(DialogMessage message)
        {
            _ = Application.Current.Dispatcher.Invoke(async () =>
            {
                TitleText.Text = message.Title;
                ContentText.Text = message.Content;
                await DialogHost.Show(this, "RootDialog");
            });
        }
    }
}
