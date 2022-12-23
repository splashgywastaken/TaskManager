using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.Model.User;

namespace TaskManagerWPF.Services.DataAccess
{
    public class UserDataAccess
    {
        public static UserDataModel UserDataModel { get; set; } = null!;

        public UserDataAccess()
        {
            
        }
    }
}
