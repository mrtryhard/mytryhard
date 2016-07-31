using MyTryHard.Models;
using MyTryHard.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTryHard.ViewModels
{
    public class AdminUserViewModel
    {
        public RegisterViewModel UserVM { get; set; }
        public bool IsAdmin { get; set; }
    }
}
