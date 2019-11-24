using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Model
{
    public class Contact
    {
        [Key]
        public string idContact { get; set; }
        public string nameContact { get; set; }
        public string phoneContact { get; set; }
        public string emailContact { get; set; }
        public string userName { get; set; }
        [ForeignKey("userName")]
        public string firstChar
        {
            get
            {
                return nameContact.Substring(0, 1).ToUpper();
            }
        }
    }
}
