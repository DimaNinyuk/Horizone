namespace HorizonSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChengTextFilm1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "Company", c => c.String(unicode: false));
            AlterColumn("dbo.Films", "RottenRating", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Films", "LinkTriller", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "LinkTriller", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.Films", "RottenRating", c => c.Int());
            AlterColumn("dbo.Films", "Company", c => c.String(maxLength: 150, unicode: false));
        }
    }
}
