using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;

namespace MontagemCurriculo.Controllers
{
    [Authorize]
    public class ExperienciasProfissionaisController : Controller
    {
        private readonly Contexto _context;

        public ExperienciasProfissionaisController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Create(int id)
        {
            ExperienciaProfissional experiencia = new ExperienciaProfissional();
            experiencia.CurriculoID = id;            
            return View(experiencia);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExperienciaProfissionalID,NomeEmpresa,Cargo,AnoInicio,AnoFim,DescricaoAtividades,CurriculoID")] ExperienciaProfissional experienciaProfissional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experienciaProfissional);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Curriculos", new { id = experienciaProfissional.CurriculoID });
            }           
            return View(experienciaProfissional);
        }
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experienciaProfissional = await _context.ExperienciasProfissionais.FindAsync(id);
            if (experienciaProfissional == null)
            {
                return NotFound();
            }            
            return View(experienciaProfissional);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExperienciaProfissionalID,NomeEmpresa,Cargo,AnoInicio,AnoFim,DescricaoAtividades,CurriculoID")] ExperienciaProfissional experienciaProfissional)
        {
            if (id != experienciaProfissional.ExperienciaProfissionalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experienciaProfissional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienciaProfissionalExists(experienciaProfissional.ExperienciaProfissionalID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculos", new { id = experienciaProfissional.CurriculoID });
            }            
            return View(experienciaProfissional);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var experienciaProfissional = await _context.ExperienciasProfissionais.FindAsync(id);
            _context.ExperienciasProfissionais.Remove(experienciaProfissional);
            await _context.SaveChangesAsync();
            return Json("Experiência excluída com sucesso");
        }

        private bool ExperienciaProfissionalExists(int id)
        {
            return _context.ExperienciasProfissionais.Any(e => e.ExperienciaProfissionalID == id);
        }
    }
}
