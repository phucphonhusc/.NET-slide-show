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
    class StudentService
    {
        /// <summary>
        /// LẤY 1 SV CÓ MÃ TƯƠNG ỨNG VỚI IDSTUDENT
        /// </summary>
        /// <param name="idStudent">MÃ SV</param>
        /// <returns>SINH VIÊN TƯƠNG ỨNG HOẶC NULL</returns>
        public static Student getStudent(string idStudent)
        {
            Student student = new Student
            {
                ID = idStudent,
                FirstName = "Phúc",
                LastName = "Phôn",
                DateOfBirth = new DateTime(1998, 1, 1),
                Gender = (int)GENDER.Male,
                PlaceOfBirth = "Huế",

            };
            return student;

        }
        /// <summary>
        /// Lấy 1 sinh viên có mã tương ứng từ file dữ liệu
        /// </summary>
        /// <param name="path">Đường dẫn tới file</param>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Sinh viên có mã tương ứng hoặc null nếu không tồn tại</returns>
        public static Student getStudent(string path, string idStudent)
        {
            var lines = File.ReadAllLines(path);
            foreach(var line in lines)
            {
                var items = line.Split(new char[] { '#' });
                if(items.Length == 6)
                {
                    var student = new Student
                    {
                        ID = items[0],
                        LastName = items[1],
                        FirstName = items[2],
                        DateOfBirth = DateTime.ParseExact(items[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Gender = (items[4] == "Male" ? GENDER.Male : (items[4] == "Female" ? GENDER.Female : GENDER.Other)),
                        PlaceOfBirth = items[5]
                    };
                    if(student.ID == idStudent)
                    {
                        return student;
                    }
                }
            }
            return null;
        }



    }
}
