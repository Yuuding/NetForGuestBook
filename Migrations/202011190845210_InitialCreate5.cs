namespace GuestBookSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guestbooks", "isPass", c => c.Boolean(nullable: false));
            AddColumn("dbo.Guestbooks", "UseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guestbooks", "UseId");
            DropColumn("dbo.Guestbooks", "isPass");
        }
    }
}
