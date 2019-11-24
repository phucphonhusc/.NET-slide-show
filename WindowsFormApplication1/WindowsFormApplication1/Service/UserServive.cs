using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Model;

namespace WindowsFormsApplication1.Service
{
    public class UserService
    {
        public static User checkLogin(string userName, string password)
        {
            var db = new AppG4Context();
            return db.Users.Where(e => (e.userName == userName && e.pasword == password)).FirstOrDefault();
        }
    }
}
