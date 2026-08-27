using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Database;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    [INotifyPropertyChanged]
    public partial class DashboardViewModel : NavigationAware
    {
        readonly ILogger _logger;
        readonly SQLite _db;
        [ObservableProperty][NotifyPropertyChangedFor(nameof(IsLoginMode))] public partial bool IsSignUpMode { get; set; }
        public bool IsLoginMode => !IsSignUpMode;
        [ObservableProperty] public partial string? LoginId { get; set; }
        [ObservableProperty] public partial string? LoginPassword { get; set; }
        [ObservableProperty] public partial string? SignUpId { get; set; }
        [ObservableProperty] public partial string? SignUpPassword { get; set; }
        [ObservableProperty] public partial string? SignUpPasswordConfirm { get; set; }

        public DashboardViewModel(ILogger<DashboardViewModel> logger, SQLite db)
        {
            _logger = logger;
            _db = db;
        }

        [RelayCommand] void GoToSignUp() => IsSignUpMode = true;
        [RelayCommand] void GoToLogin() => IsSignUpMode = false;

        [RelayCommand]
        void Login()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("Login Error: {m}", ex.Message);
            }
        }

        [RelayCommand]
        async Task SignUp()
        {
            try
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

                var existing = await _db.IsUserExist(SignUpId);
                if (existing)
                {
                    _logger.LogWarning("id already exists: {id}", SignUpId);
                    return;
                }

                await _db.InsertUser(SignUpId, SignUpPassword);

                _logger.LogInformation("sign up: {id}", SignUpId);

                SignUpId = null;
                SignUpPassword = null;
                SignUpPasswordConfirm = null;
                IsSignUpMode = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("SignUp Error: {m}", ex.Message);
            }
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            // show before login id
        }
    }
}
