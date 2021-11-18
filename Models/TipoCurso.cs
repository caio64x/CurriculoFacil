using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MontagemCurriculo.Models
{
    public class TipoCurso
    {
        public int TipoCursoID { get; set; }
        public string Tipo { get; set; }
        public ICollection<FormacaoAcademica>FormacoesAcademicas { get; set; }
    }
}
