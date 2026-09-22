using CommunityToolkit.Mvvm.ComponentModel;

namespace UI.Model
{
    public partial class UserSession : ObservableObject
    {
        [ObservableProperty] public partial string? CurrentUserId { get; set; }
    }
}
