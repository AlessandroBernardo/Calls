using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Chamado.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Chamado.DAL
{
    public class ChamadoContexto : DbContext
    {
        public ChamadoContexto() 
            : base ("name = DefaultConnection")
        {
    
        }

        public DbSet<Chamado.Models.Chamado> Chamados { get; set; }
        public DbSet<Chamado.Models.Mod_Anexo> Anexos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();            
            
            //"ToTable" = será o nome da tabela no Banco | haskey define que a PK é o campo IdChamado
            modelBuilder.Entity<Chamado.Models.Chamado>().ToTable("Tb_Chamados").HasKey(x => x.IdChamado);            
            
            //define que a PK é a IdAnexo
            modelBuilder.Entity<Mod_Anexo>().ToTable("Tb_Anexos").HasKey(x => x.IdAnexo);
                        
            //relacionamento 1 : N entre Chamado e Anexo {Um chamado pode ter muitos anexos}
            modelBuilder.Entity<Chamado.Models.Chamado>().HasMany(a => a.Anexos).WithRequired(c => c.Chamado)
                .HasForeignKey(x => x.IdChamado);       

        }
    }
}