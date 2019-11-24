namespace WindowsFormsApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class khoitaodb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        idContact = c.String(nullable: false, maxLength: 128),
                        nameContact = c.String(),
                        phoneContact = c.String(),
                        emailContact = c.String(),
                    })
                .PrimaryKey(t => t.idContact);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
