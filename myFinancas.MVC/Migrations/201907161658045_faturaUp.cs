namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class faturaUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.faturas", "mes_referente", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.faturas", "mes_referente");
        }
    }
}
