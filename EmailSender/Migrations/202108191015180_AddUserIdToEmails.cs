namespace EmailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToEmails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emails", "UserId");
        }
    }
}
