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
    public class CurriculoMap : IEntityTypeConfiguration<Curriculo>
    {
        public void Configure(EntityTypeBuilder<Curriculo> builder)
        {   
            //chave primaria
            builder.HasKey(c => c.CurriculoID);

            //propriedade comum
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(50);
            //informacao unica
            builder.HasIndex(c => c.Nome).IsUnique();

            //tem um usuario com varios curriculos
            builder.HasOne(c => c.Usuario).WithMany(c => c.Curriculos).HasForeignKey(c => c.UsuarioID);
            //tem muitos objetivos com um curriculo
            builder.HasMany(c => c.Objetivos).WithOne(c => c.Curriculo).OnDelete(DeleteBehavior.Cascade);
            //tem muitas formaceoes academicas com um curriculo
            builder.HasMany(c => c.FormacoesAcademicas).WithOne(c => c.Curriculo).OnDelete(DeleteBehavior.Cascade);
            //tem muitas expericencias profissionais com um curriculo
            builder.HasMany(c => c.ExperienciaProfissionais).WithOne(c => c.Curriculo).OnDelete(DeleteBehavior.Cascade);
            //tem muitos Idiomas com um curriculo
            builder.HasMany(c => c.Idiomas).WithOne(c => c.Curriculo).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Curriculos");
        }
    }
}
