namespace HorizonSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class any : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "Date", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "Date", c => c.DateTime());
        }
    }
}
