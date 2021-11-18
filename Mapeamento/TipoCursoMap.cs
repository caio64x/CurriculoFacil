using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MontagemCurriculo.Mapeamento
{
    public class TipoCursoMap : IEntityTypeConfiguration<TipoCurso>
    {
        public void Configure(EntityTypeBuilder<TipoCurso> builder)
        {
            //chave primaria
            builder.HasKey(tc => tc.TipoCursoID);

            //propriedade comum
            builder.Property(tc => tc.Tipo).IsRequired();
            //informacao unica
            builder.HasIndex(tc => tc.Tipo).IsUnique();
            //tem muitas formacoes academucas com um tipo de curso
            builder.HasMany(tc => tc.FormacoesAcademicas).WithOne(tc => tc.TipoCurso).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("TipoCursos");
        }
    }
}
