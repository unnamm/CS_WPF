using Common.Message;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace UI.View
{
    public partial class MainWindowView : Window
    {
        private bool _isExiting;

        public MainWindowView(ContentView content)
        {
            InitializeComponent();
            Style = (Style)FindResource("MaterialDesignWindow");

            ContentFrame.Content = content;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            WeakReferenceMessenger.Default.Send(new MainWindowRenderedMessage());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_isExiting == true)
                return;

            _isExiting = true;
            e.Cancel = true;
            base.OnClosing(e);
            WeakReferenceMessenger.Default.Send(new MainViewCloseMessage());
        }
    }
}
