using MyTryHard.ViewModels.Account;

namespace MyTryHard.ViewModels
{
    public class AdminUserViewModel
    {
        public RegisterViewModel UserVM { get; set; }
        public bool IsAdmin { get; set; }
    }
}
