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
        public const string DialogIdentifire = "RootDialog";

        public DialogView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(DialogMessage message)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                if (DialogHost.IsDialogOpen(DialogIdentifire))
                {
                    DialogHost.Close(DialogIdentifire);
                }

                TitleText.Text = message.Title;
                ContentText.Text = message.Content;
                await DialogHost.Show(this, DialogIdentifire);
            });
        }
    }
}
