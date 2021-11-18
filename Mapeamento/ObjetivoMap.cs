using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemCurriculo.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MontagemCurriculo.Mapeamento
{
    public class ObjetivoMap : IEntityTypeConfiguration<Objetivo>
    {
        void IEntityTypeConfiguration<Objetivo>.Configure(EntityTypeBuilder<Objetivo> builder)
        {
            //chave primaria
            builder.HasKey(o => o.ObjetivoID);

            //propriedade comum
            builder.Property(o => o.Descrição).IsRequired().HasMaxLength(1000);

            //tem um curriculo com varios objetivos
            builder.HasOne(o => o.Curriculo).WithMany(o => o.Objetivos).HasForeignKey(o => o.CurriculoID);

            builder.ToTable("Objetivos");
        }
    }
}
