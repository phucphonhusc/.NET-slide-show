using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Model
{
    public class User
    {
        [Key]
        public string userName { get; set; }
        public string pasword { get; set; }

    }
}
