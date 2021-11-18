using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace MontagemCurriculo.Mapeamento
{
    public class ExperienciaProfissionalMap : IEntityTypeConfiguration<ExperienciaProfissional>
    {
        public void Configure(EntityTypeBuilder<ExperienciaProfissional> builder)
        {
            //chave primaria
            builder.HasKey(e => e.ExperienciaProfissionalID);

            //propriedade comum
            builder.Property(e => e.NomeEmpresa).IsRequired().HasMaxLength(50);
            //propriedade comum
            builder.Property(e => e.Cargo).IsRequired().HasMaxLength(50);
            //propriedade comum
            builder.Property(e => e.AnoInicio).IsRequired().HasMaxLength(50);
            //propriedade comum
            builder.Property(e => e.AnoFim).IsRequired().HasMaxLength(50);
            //propriedade comum
            builder.Property(e => e.DescricaoAtividades).IsRequired().HasMaxLength(500);

            //tem um curriculo com varias experiencias
            builder.HasOne(e => e.Curriculo).WithMany(e => e.ExperienciaProfissionais).HasForeignKey(e => e.CurriculoID);

            builder.ToTable("ExperienciasProfissionais");
        }
    }
}
