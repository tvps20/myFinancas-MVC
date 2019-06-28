namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fatura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cartoes", "is_ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.faturas", "is_fechada", c => c.Boolean(nullable: false));
            AddColumn("dbo.faturas", "is_ativo", c => c.Boolean(nullable: false));
            DropColumn("dbo.cartoes", "ativo");
            DropColumn("dbo.faturas", "ativo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.faturas", "ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.cartoes", "ativo", c => c.Boolean(nullable: false));
            DropColumn("dbo.faturas", "is_ativo");
            DropColumn("dbo.faturas", "is_fechada");
            DropColumn("dbo.cartoes", "is_ativo");
        }
    }
}
