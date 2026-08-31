using CommunityToolkit.Mvvm.ComponentModel;
using Database;

namespace View
{
    public partial class UserRowViewModel : ObservableObject
    {
        public string Id { get; }
        [ObservableProperty] public partial string? Rank { get; set; }
        [ObservableProperty] public partial int? RoleId { get; set; }

        public UserRowViewModel(UserInfo info)
        {
            Id = info.Id;
            Rank = info.Rank;
            RoleId = info.RoleId;
        }
    }
}
