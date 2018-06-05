namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "Subject", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "Subject", c => c.String());
        }
    }
}
