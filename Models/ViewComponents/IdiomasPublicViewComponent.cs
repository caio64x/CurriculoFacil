using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.ViewComponents
{
    public class IdiomasPublicViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public IdiomasPublicViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _contexto.Idiomas.Where(i => i.CurriculoID == id).ToListAsync());
        }
    }
}
