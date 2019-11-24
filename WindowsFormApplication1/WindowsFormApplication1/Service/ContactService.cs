using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Model;

namespace WindowsFormsApplication1.Service
{
    class ContactService
    {
        public static List<Contact> getContact(string path)
        {
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                List<Contact> lsContact = new List<Contact>();
                foreach(var line in lines)
                {
                    var items = line.Split(new char[] { '#' });
                    Contact ct = new Contact
                    {
                        idContact = items[0],
                        nameContact= items[1],
                        phoneContact = items[2],
                        emailContact = items[3]
                    };
                    lsContact.Add(ct);
                }
                return lsContact;
            }
            else
            {
                return null;
            }
        }
        public static List<Contact> getSearchContact(string pathDataFile, string search)
        {
            if (File.Exists(pathDataFile))
            {
                var lines = File.ReadAllLines(pathDataFile);
                List<Contact> lsContact = new List<Contact>();
                foreach (var line in lines)
                {
                    var items = line.Split(new char[] { '#' });
                    Contact ct = new Contact
                    {
                        idContact = items[0],
                        nameContact = items[1],
                        phoneContact = items[2],
                        emailContact = items[3]
                    };

                    if (ct.nameContact.ToLower().Contains(search.ToLower()))
                    {
                        lsContact.Add(ct);
                    }


                }
                return lsContact;
            }
            else
                return null;
        }

        //Delete
        public static void deleteContact(string pathContactFile, string idContact)
        {
            if (File.Exists(pathContactFile))
            {
                var lines = File.ReadAllLines(pathContactFile);
                File.WriteAllText(pathContactFile, "");
                foreach (var line in lines)
                {
                    var items = line.Split(new char[] { '#' });
                    if (items[0] != idContact)
                    {

                        File.AppendAllText(pathContactFile, line + "\r\n");
                    }
                }

            }
        }
        //Add
        public static void addContact(string pathContactFile, string name, string phone, string email)
        {


            int x = 0;
            var lines = File.ReadAllLines(pathContactFile);
            foreach (var line in lines)
            {
                var items = line.Split(new char[] { '#' });
                if (int.Parse(items[0]) > x)
                {

                    x = int.Parse(items[0]);
                }

            }
            var idContact = x + 1;
            string lineContact = idContact + "#" + name + "#" + phone + "#" + email;
            if (File.Exists(pathContactFile))
            {
                File.AppendAllText(pathContactFile, lineContact + "\n");
            }
        }
        //Edit
        public static void editContact(string idContact, string pathDataFile, string name, string phone, string email)
        {
            if (File.Exists(pathDataFile))
            {
                var lines = File.ReadAllLines(pathDataFile);
                File.WriteAllText(pathDataFile, "");

                foreach (var line in lines)
                {
                    var items = line.Split(new char[] { '#' });
                    if (items[0] != idContact)
                    {

                        File.AppendAllText(pathDataFile, line + "\r\n");

                    }
                    else
                    {

                        string editContent = idContact + "#" + name + "#" + phone + "#" + email;
                        File.AppendAllText(pathDataFile, editContent + "\r\n");

                    }
                }

            }
        }
        public static List<Contact> GetContactDB(string userName)
        {
            var db = new AppG4Context();
            return db.Contacts.Where(e => e.userName.ToLower().CompareTo(userName) == 0).ToList();
        }
        public static List<Contact> GetSearchContactDB(string search, string username)
        {
            var db = new AppG4Context();
            return db.Contacts.Where(e => (e.nameContact.ToLower().Contains(search.ToLower()) && e.userName == username)).ToList();
        }
        public static void AddContactDB(string name, string phone, string email, string userName)
        {
            var db = new AppG4Context();
            var cre = db.Contacts.Create();
            cre.idContact = Guid.NewGuid().ToString();
            cre.nameContact = name;
            cre.phoneContact = phone;
            cre.emailContact = email;
            cre.userName = userName;
            db.Contacts.Add(cre);
            db.SaveChanges();
        }
        public static void EditContactDB(string IDContact, string name, string phone, string email)
        {
            var db = new AppG4Context();
            var result = db.Contacts.SingleOrDefault(a => a.idContact.Equals(IDContact));
            if (result != null)
            {
                result.nameContact = name;
                result.phoneContact = phone;
                result.emailContact = email;
                db.SaveChanges();
            }
        }
        public static void DeleteContactDB(string idContact)
        {
            var db = new AppG4Context();
            var itemToRemove = db.Contacts.SingleOrDefault(x => x.idContact.Equals(idContact));
            if (itemToRemove != null)
            {
                db.Contacts.Remove(itemToRemove);
                db.SaveChanges();
            }


        }
    }
}
