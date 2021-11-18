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
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //chave primaria
            builder.HasKey(u => u.UsuarioID);

            //propriedade comum
            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            //informacao unica
            builder.HasIndex(u => u.Email).IsUnique();

            //propriedade comum
            builder.Property(u => u.Senha).IsRequired().HasMaxLength(50);

            //tem muitas formaceoes academicas com um curriculo
            builder.HasMany(u => u.Curriculos).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.Cascade);
            //tem muitas formaceoes academicas com um curriculo
            builder.HasMany(u => u.InformacoesLogin).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Usuarios");

        }
    }
}
