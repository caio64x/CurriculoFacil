using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Threading.Tasks;

namespace MontagemCurriculo.Mapeamento
{
    public class InformacaoLoginMap : IEntityTypeConfiguration<Models.InformacaoLogin>
    {
        public void Configure(EntityTypeBuilder<InformacaoLogin> builder)
        {
            //chave primaria
            builder.HasKey(i => i.InformacaoLoginID);

            //propriedade comum
            builder.Property(i => i.EnderecoIP).IsRequired();
            //propriedade comum
            builder.Property(i => i.Horario).IsRequired();
            //propriedade comum
            builder.Property(i => i.Data).IsRequired();

            //tem um usuario com varias informacoes de login
            builder.HasOne(i => i.Usuario).WithMany(i => i.InformacoesLogin).HasForeignKey(i => i.UsuarioID);

            builder.ToTable("InformacoesLogin");
        }
    }
}
