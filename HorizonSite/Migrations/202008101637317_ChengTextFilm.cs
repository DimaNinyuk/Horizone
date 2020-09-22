namespace HorizonSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChengTextFilm : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "Company", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "Company", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
