namespace WindowsFormsApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WindowsFormsApplication1.Model.AppG4Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WindowsFormsApplication1.Model.AppG4Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Contacts.AddOrUpdate(
                new Model.Contact
                {
                    idContact = "1",
                    nameContact ="Phon",
                    phoneContact="0967074504",
                    emailContact="nvpp15598@gmail.com",
                    userName = "phucphon@gmail.com"
                },
                new Model.Contact
                {
                    idContact = "2",
                    nameContact = "Phu",
                    phoneContact = "096702354",
                    emailContact = "phunguyen@gmail.com",
                    userName = "admin"
                }

            );
            context.Users.AddOrUpdate(
                new Model.User
                {
                    userName = "phucphon@gmail.com",
                    pasword = "123"
                },
                new Model.User
                {
                    userName = "admin",
                    pasword = "123"
                }
            );
        
                

        }
    }
}
