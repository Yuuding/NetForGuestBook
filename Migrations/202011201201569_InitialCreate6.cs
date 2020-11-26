namespace GuestBookSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guestbooks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Guestbooks", new[] { "User_UserId" });
            RenameColumn(table: "dbo.Guestbooks", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Guestbooks", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Guestbooks", "UserId");
            AddForeignKey("dbo.Guestbooks", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            DropColumn("dbo.Guestbooks", "AuthorEmail");
            DropColumn("dbo.Guestbooks", "UseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guestbooks", "UseId", c => c.Int(nullable: false));
            AddColumn("dbo.Guestbooks", "AuthorEmail", c => c.String(nullable: false));
            DropForeignKey("dbo.Guestbooks", "UserId", "dbo.Users");
            DropIndex("dbo.Guestbooks", new[] { "UserId" });
            AlterColumn("dbo.Guestbooks", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Guestbooks", name: "UserId", newName: "User_UserId");
            CreateIndex("dbo.Guestbooks", "User_UserId");
            AddForeignKey("dbo.Guestbooks", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
