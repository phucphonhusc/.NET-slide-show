namespace WindowsFormsApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        pasword = c.String(),
                    })
                .PrimaryKey(t => t.userName);
            
            AddColumn("dbo.Contacts", "idUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "idUser");
            DropTable("dbo.Users");
        }
    }
}
