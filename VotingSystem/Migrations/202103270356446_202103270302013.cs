namespace VotingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202103270302013 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.States", "description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.States", "description", c => c.String());
        }
    }
}
