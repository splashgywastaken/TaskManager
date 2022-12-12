using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private string _title = "Home page";
        private UserDataAccess _userDataAccessService;
        private bool _isUserDataLoaded;

        public HomeViewModel()
        {
            _userDataAccessService = 
                 App.AppHost!.Services.GetService(typeof(UserDataAccess)) as UserDataAccess ?? throw new InvalidOperationException();
            IsUserDataLoaded = true;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public bool IsUserDataLoaded
        {
            get => _isUserDataLoaded;
            set
            {
                _isUserDataLoaded = value;
                OnPropertyChanged();
            }
        }
    }
}
