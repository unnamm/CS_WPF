using Common.Message;
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

namespace UI.View
{
    public partial class DialogView : UserControl, IRecipient<DialogMessage>
    {
        public DialogView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.RegisterAll(this);
            SampleTest();
        }

        private async void SampleTest()
        {
            await Task.Delay(1000);
            WeakReferenceMessenger.Default.Send(new DialogMessage("title", "Content"));
            await Task.Delay(1000);
            WeakReferenceMessenger.Default.Send(new DialogMessage("title2", "Content2"));
        }

        public void Receive(DialogMessage message)
        {
            _ = Application.Current.Dispatcher.Invoke(async () =>
            {
                const string dialogIdentifier = "RootDialog";

                if (DialogHost.IsDialogOpen(dialogIdentifier))
                {
                    DialogHost.Close(dialogIdentifier);
                }

                TitleText.Text = message.Title;
                ContentText.Text = message.Content;
                await DialogHost.Show(this, dialogIdentifier);
            });
        }
    }
}
