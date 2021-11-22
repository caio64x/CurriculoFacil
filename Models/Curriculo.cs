using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace MontagemCurriculo.Models
{
    public class Curriculo
    {
        public int CurriculoID { get; set; }

        [Required(ErrorMessage = "Campo obrigtório")]
        [StringLength(50, ErrorMessage = "Use menos caracteres")]
        [Remote("NomeExiste", "Curriculos")]
        public string Nome { get; set; }

        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        


        public ICollection<Objetivo> Objetivos { get; set; }
        public ICollection<FormacaoAcademica> FormacoesAcademicas { get; set; }
        public ICollection<ExperienciaProfissional> ExperienciaProfissionais { get; set; }
        public ICollection<Idioma> Idiomas { get; set; }

    }
}
