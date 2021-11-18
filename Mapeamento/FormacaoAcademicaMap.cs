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
    public class FormacaoAcademicaMap : IEntityTypeConfiguration<FormacaoAcademica>
    {
        public void Configure(EntityTypeBuilder<FormacaoAcademica> builder)
        {
            //chave primaria
            builder.HasKey(f => f.FormacaoAcademicaID);

            //propriedade comum
            builder.Property(f => f.Instituicao).IsRequired().HasMaxLength(50);
            //propriedade comum
            builder.Property(f => f.AnoInicio).IsRequired();
            //propriedade comum
            builder.Property(f => f.AnoFim).IsRequired();
            //propriedade comum
            builder.Property(f => f.NomeCurso).IsRequired().HasMaxLength(50);

            //tem um tipo de curso com varias formacoes academicas
            builder.HasOne(f => f.TipoCurso).WithMany(f => f.FormacoesAcademicas).HasForeignKey(f => f.TipoCursoID);
            builder.HasOne(f => f.Curriculo).WithMany(f => f.FormacoesAcademicas).HasForeignKey(f => f.CurriculoID);

            builder.ToTable("FormacoesAcademicas");
        }
    }
}
