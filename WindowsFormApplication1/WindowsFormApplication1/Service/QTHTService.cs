using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Model;

namespace WindowsFormsApplication1.Service
{
    class QTHTService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idStudent"></param>
        /// <returns></returns>
        public static List<QTHT> getQTHT(string idStudent)
        {
            List<QTHT> list = new List<QTHT>();
            for(int i=1; i<=12; i++)
            {
                QTHT qtht = new QTHT
                {
                    ID = i.ToString(),
                    FromYear = 2000 + i,
                    ToYear = 2001 + i,
                    SchoolName = "Hương Thủy",
                    IDStudent = idStudent,
                    
                };
                list.Add(qtht);
                
            }
            return list;
        }
        /// <summary>
        /// lay danh sach sinh vien vao file
        /// </summary>
        /// <param name="path">duong dan file</param>
        /// <param name="idStudent"></param>
        /// <returns></returns>
        public static List<QTHT> getQTHT(string path,string idStudent)
        {
            List<QTHT> list = new List<QTHT>();
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    var qtht = QTHT.Parse(line);    
                   if (qtht.IDStudent == idStudent)
                   {
                        list.Add(qtht);
                   }               
                }
                return list;
            }
            else
            {
                return null;
            }           
        }
        public static void Remove(string path, QTHT qtht)
        {
            if (File.Exists(path))
            {
                List<string> rs = new List<string>();
                var lines = File.ReadAllLines(path);
                foreach(var line in lines)
                {
                    var data = QTHT.Parse(line);
                    if(data.ID != qtht.ID)
                    {
                        rs.Add(line);
                    }
                }
                File.WriteAllLines(path, rs);
            }
            else
            {
                throw new Exception("File du lieu khong ton tai!");
            }
        }
        //them qtht
        /*public static void Add(string path, QTHT qtht)
        {
            if (!File.Exists(path))
                throw new Exception("File không tồn tại!");
            List<string> rs = new List<string>();
            var lines = File.ReadAllLines(path).ToList<string>();
            foreach (var line in lines)
            {
                if (QTHT.Parse(line).ID == qtht.ID)
                {
                    throw new Exception("Đã tồn tại ID!");
                }
                else
                    rs.Add(line);
            }
            qtht.ID = Guid.NewGuid().ToString();
            rs.Add(qtht.ToString());
            File.WriteAllLines(path, rs);
        }
        public static void Update(string path, QTHT qtht)
        {
            if (!File.Exists(path))
                throw new Exception("File không tồn tại!");
            List<string> rs = new List<string>();
            var lines = File.ReadAllLines(path).ToList<string>();
            foreach (var line in lines)
            {
                if (QTHT.Parse(line).ID == qtht.ID)
                {
                    rs.Add(qtht.ToString());
                }
                else
                    rs.Add(line);
            }
            File.WriteAllLines(path, rs);
        } */
        public static void Add(string path, int tunam, int dennam, string noihoc)
        {
            int x = 0;
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var items = line.Split(new char[] { '#' });
                if (int.Parse(items[0]) > x)
                {

                    x = int.Parse(items[0]);
                }

            }
            var idqtht = x + 1;
            string lineqtht = idqtht + "#" + tunam + "#" + dennam + "#" + noihoc;
            if (File.Exists(path))
            {
                File.AppendAllText(path, lineqtht + "\n");
            }
        }
        public static void Update(string idqtht, string path, int tunam, int dennam, string noihoc)
        {
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                File.WriteAllText(path, "");

                foreach (var line in lines)
                {
                    var items = line.Split(new char[] { '#' });
                    if (items[0] != idqtht)
                    {

                        File.AppendAllText(path, line + "\r\n");

                    }
                    else
                    {

                        string updateContent = idqtht + "#" + tunam + "#" + dennam + "#" + noihoc;
                        File.AppendAllText(path, updateContent + "\r\n");

                    }
                }

            }
        }

    }
}
