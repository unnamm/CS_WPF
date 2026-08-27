using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    [INotifyPropertyChanged]
    public partial class DashboardViewModel : NavigationAware
    {
        public ObservableCollection<Log.LogEntry> LogList { get; set; }
        readonly ILogger _logger;
        [ObservableProperty][NotifyPropertyChangedFor(nameof(IsLoginMode))] private bool _isSignUpMode;
        public bool IsLoginMode => !IsSignUpMode;
        [ObservableProperty] string? _loginId;
        [ObservableProperty] string? _loginPassword;
        [ObservableProperty] string? _signUpId;
        [ObservableProperty] string? _signUpPassword;
        [ObservableProperty] string? _signUpPasswordConfirm;
        [ObservableProperty] string? _message;

        public DashboardViewModel(Log.ViewLoggerProvider logProvider, ILogger<DashboardViewModel> logger)
        {
            LogList = logProvider.Logs;
            _logger = logger;
        }

        [RelayCommand]
        private void GoToSignUp()
        {
            Message = null;
            IsSignUpMode = true;
        }

        [RelayCommand]
        private void GoToLogin()
        {
            Message = null;
            IsSignUpMode = false;
        }

        [RelayCommand]
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(LoginId))
            {
                Message = "id id empty";
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginPassword))
            {
                Message = "password id empty";
                return;
            }

            Message = string.Empty;
            _logger.LogInformation("login attempt: {id}", LoginId);
        }

        [RelayCommand]
        private void SignUp()
        {
            if (string.IsNullOrWhiteSpace(SignUpId))
            {
                Message = "id is empty";
                return;
            }
            if (string.IsNullOrWhiteSpace(SignUpPassword))
            {
                Message = "password is empty";
                return;
            }

            if (SignUpPassword != SignUpPasswordConfirm)
            {
                Message = "password is not confirm";
                return;
            }

            _logger.LogInformation("sign up: {id}", SignUpId);

            SignUpId = null;
            SignUpPassword = null;
            SignUpPasswordConfirm = null;

            Message = "complete sign up";
            IsSignUpMode = false;
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            _logger.LogInformation("to DashboardViewModel");
        }

        public override void OnNavigatedFrom()
        {
            base.OnNavigatedFrom();
            _logger.LogInformation("from DashboardViewModel");
        }
    }
}
