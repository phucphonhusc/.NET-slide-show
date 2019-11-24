namespace WindowsFormsApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "userName", c => c.String());
            DropColumn("dbo.Contacts", "idUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "idUser", c => c.String());
            DropColumn("dbo.Contacts", "userName");
        }
    }
}
