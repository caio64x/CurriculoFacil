using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.ViewComponents
{
    public class FormacoesAcademicasPublicViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public FormacoesAcademicasPublicViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _contexto.FormacoesAcademicas.Include(f => f.TipoCurso).Where(f => f.CurriculoID == id).ToListAsync());
        }
    }
}