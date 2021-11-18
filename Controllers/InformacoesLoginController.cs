using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontagemCurriculo.Controllers
{
    public class InformacoesLoginController : Controller
    {
        private readonly Contexto _contexto;

        public InformacoesLoginController(Contexto contexto)
        {
            _contexto = contexto;
        }


        public async Task<IActionResult> Index()
        {
            var usuarioID = HttpContext.Session.GetInt32("UsuarioID");

            return View(await _contexto.InformacoesLogin.Include(u => u.Usuario).Where(i => i.UsuarioID == usuarioID).ToListAsync());


        }

        public IActionResult DownloadDados()
        {
            var usuarioID = HttpContext.Session.GetInt32("UsuarioID");

            var dados = _contexto.InformacoesLogin.Include(u => u.Usuario).Where(i => i.UsuarioID == usuarioID).ToList();
            StringBuilder arquivo = new StringBuilder();

            arquivo.AppendLine("EnderecoIP;Data;Horario");

            foreach (var item in dados)
            {
                arquivo.AppendLine(item.EnderecoIP + ";" + item.Data + ";" + item.Horario);
            }
            return File(Encoding.ASCII.GetBytes(arquivo.ToString()), "text/csv", "dados.csv");
        }
    }
}
