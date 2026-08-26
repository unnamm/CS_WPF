using System.Windows;

namespace View
{
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        public void SetStatus(string text) => StatusText.Text = text;
    }
}
