using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MontagemCurriculo.Models
{
    public class Objetivo
    {
        public int ObjetivoID { get; set; }

        [Required(ErrorMessage = "Descrição obrigatória")]
        [StringLength(1000, ErrorMessage = "Descrição muito longa")]
        [DataType(DataType.MultilineText)]
        public string Descrição { get; set; }

        public int CurriculoID { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
