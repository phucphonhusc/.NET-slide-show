using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Model
{
    public class AppG4Context : DbContext
    {
        public AppG4Context() : base("Data Source=DESKTOP-8GEN06L;Initial Catalog=DBcontact;Persist Security Info=True;User ID=sa; Password=123")
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
