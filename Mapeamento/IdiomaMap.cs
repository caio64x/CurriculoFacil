using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MontagemCurriculo.Mapeamento
{
    public class IdiomaMap : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> builder)
        {
            //chave primaria
            builder.HasKey(i => i.IdiomaID);

            //propriedade comum
            builder.Property(i => i.Nome).IsRequired().HasMaxLength(50);
            
            //propriedade comum
            builder.Property(i => i.Nivel).IsRequired().HasMaxLength(50);

            //tem um curriculo com varios idiomas
            builder.HasOne(i => i.Curriculo).WithMany(i => i.Idiomas).HasForeignKey(i => i.CurriculoID);

            builder.ToTable("Idiomas");
        }
    }
}
