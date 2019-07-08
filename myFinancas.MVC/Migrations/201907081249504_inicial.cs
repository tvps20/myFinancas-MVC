namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dividas", "descricao", c => c.String());
            DropColumn("dbo.dividas", "observacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.dividas", "observacao", c => c.String());
            DropColumn("dbo.dividas", "descricao");
        }
    }
}
