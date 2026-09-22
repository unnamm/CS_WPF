using System.Windows;

namespace UI
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
