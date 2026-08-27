using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    [INotifyPropertyChanged]
    public partial class DashboardViewModel : NavigationAware
    {
        readonly ILogger _logger;
        [ObservableProperty][NotifyPropertyChangedFor(nameof(IsLoginMode))] public partial bool IsSignUpMode { get; set; }
        public bool IsLoginMode => !IsSignUpMode;
        [ObservableProperty] public partial string? LoginId { get; set; }
        [ObservableProperty] public partial string? LoginPassword { get; set; }
        [ObservableProperty] public partial string? SignUpId { get; set; }
        [ObservableProperty] public partial string? SignUpPassword { get; set; }
        [ObservableProperty] public partial string? SignUpPasswordConfirm { get; set; }

        public DashboardViewModel(ILogger<DashboardViewModel> logger)
        {
            _logger = logger;
        }

        [RelayCommand]
        void GoToSignUp()
        {
            IsSignUpMode = true;
        }

        [RelayCommand]
        void GoToLogin()
        {
            IsSignUpMode = false;
        }

        [RelayCommand]
        void Login()
        {
            if (string.IsNullOrWhiteSpace(LoginId))
            {
                _logger.LogWarning("ID is empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginPassword))
            {
                _logger.LogWarning("Password id empty");
                return;
            }

            _logger.LogInformation("login attempt: {id}", LoginId);
        }

        [RelayCommand]
        void SignUp()
        {
            if (string.IsNullOrWhiteSpace(SignUpId))
            {
                _logger.LogWarning("ID is empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(SignUpPassword))
            {
                _logger.LogWarning("Password id empty");
                return;
            }

            if (SignUpPassword != SignUpPasswordConfirm)
            {
                _logger.LogWarning("password is not confirm");
                return;
            }

            _logger.LogInformation("sign up: {id}", SignUpId);

            SignUpId = null;
            SignUpPassword = null;
            SignUpPasswordConfirm = null;
            IsSignUpMode = false;
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            // show before login id
        }
    }
}
