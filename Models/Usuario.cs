using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace MontagemCurriculo.Models
{
    public class Usuario
    {   
        public int UsuarioID { get; set; }
        [Required(ErrorMessage = "Campo obrigtório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Remote("UsuarioExiste","Usuarios")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigtório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public ICollection<InformacaoLogin>InformacoesLogin { get; set; }
        public ICollection<Curriculo> Curriculos { get; set; }
    }
}
