using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.Models.ViewComponents
{
    public class ObjetivosPublicViewComponent:ViewComponent
    {
        private readonly Contexto _contexto;

        public ObjetivosPublicViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _contexto.Objetivos.Where(o => o.CurriculoID == id).ToListAsync());
        }
    }
}
