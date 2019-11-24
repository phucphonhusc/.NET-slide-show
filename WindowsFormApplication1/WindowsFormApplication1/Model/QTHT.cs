using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Model
{
    public class QTHT
    {
        public string ID { get; set; }
        public int FromYear { get; set; }
        public int ToYear{ get; set; }
        public string SchoolName { get; set; }
        public string IDStudent { get; set; }
        public virtual Student Student { get; set; }

        public static QTHT Parse(string dataString)
        {
            var items = dataString.Split(new char[] { '#' });
            return new QTHT
            {
                ID = items[0],
                FromYear = int.Parse(items[1]),
                ToYear = int.Parse(items[2]),
                SchoolName = items[3],
                IDStudent = items[4]
            };
        }
        public string Parse()
        {
            return string.Format("{0}#{1}#{2}#{3}#{4}",
                this.ID,
                this.FromYear,
                this.ToYear,
                this.SchoolName,
                this.IDStudent);
        }

    }
}
