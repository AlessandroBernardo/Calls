namespace Chamado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteracoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anexos",
                c => new
                    {
                        IdAnexo = c.Int(nullable: false, identity: true),
                        Extensao = c.String(),
                        CaminhoAnexo = c.String(),
                        Chamado_IdChamado = c.Int(),
                    })
                .PrimaryKey(t => t.IdAnexo)
                .ForeignKey("dbo.Chamados", t => t.Chamado_IdChamado)
                .Index(t => t.Chamado_IdChamado);
            
            CreateTable(
                "dbo.Chamados",
                c => new
                    {
                        IdChamado = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Motivo = c.String(nullable: false),
                        Usuario = c.String(),
                        Sistema = c.String(nullable: false),
                        NumChamado = c.String(),
                    })
                .PrimaryKey(t => t.IdChamado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anexos", "Chamado_IdChamado", "dbo.Chamados");
            DropIndex("dbo.Anexos", new[] { "Chamado_IdChamado" });
            DropTable("dbo.Chamados");
            DropTable("dbo.Anexos");
        }
    }
}
