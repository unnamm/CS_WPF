using CommunityToolkit.Mvvm.ComponentModel;

namespace View.Model
{
    public partial class UserSession : ObservableObject
    {
        [ObservableProperty] public partial string? CurrentUserId { get; set; }
    }
}
