using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.ViewComponents
{
    public class ExperienciasProfissionaisViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public ExperienciasProfissionaisViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _contexto.ExperienciasProfissionais.Where(ep => ep.CurriculoID == id).ToListAsync());
        }
    }
}
