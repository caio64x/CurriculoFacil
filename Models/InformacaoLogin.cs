using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MontagemCurriculo.Models
{
    public class InformacaoLogin
    {
        public int InformacaoLoginID { get; set; }
        public string EnderecoIP { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

    }
}
