namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lancamentos2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.lancamentos", "descricao", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.lancamentos", "descricao", c => c.String());
        }
    }
}
