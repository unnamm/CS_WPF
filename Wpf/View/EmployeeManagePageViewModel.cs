using CommunityToolkit.Mvvm.ComponentModel;
using Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    [INotifyPropertyChanged]
    public partial class EmployeeManagePageViewModel : NavigationAware
    {
        readonly SQLite _db;
        readonly ILogger _logger;

        public ObservableCollection<UserRowViewModel> Users { get; } = [];
        public ObservableCollection<RoleInfo> Roles { get; } = [];

        public EmployeeManagePageViewModel(SQLite db, ILogger<EmployeeManagePageViewModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        public override async Task OnNavigatedToAsync()
        {
            await base.OnNavigatedToAsync();

            try
            {
                Roles.Clear();
                foreach (var role in await _db.GetRoles())
                    Roles.Add(role);

                Users.Clear();
                foreach (var user in await _db.GetUsers())
                    Users.Add(new UserRowViewModel(user));
            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeManage load error: {m}", ex.Message);
            }
        }

        public override async Task OnNavigatedFromAsync()
        {
            try
            {
                foreach (var user in Users)
                    await _db.UpdateUserAsync(user.Id, user.Rank, user.RoleId);
            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeManage save error: {m}", ex.Message);
            }

            await base.OnNavigatedFromAsync();
        }
    }
}
