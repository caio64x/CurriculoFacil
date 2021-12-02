using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;

namespace MontagemCurriculo.Controllers
{
    [Authorize]
    public class IdiomasController : Controller
    {
        private readonly Contexto _context;

        public IdiomasController(Contexto context)
        {
            _context = context;
        } 

        public IActionResult Create(int id)
        {
            Idioma idioma = new Idioma();
            idioma.CurriculoID = id;            
            return View(idioma);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdiomaID,Nome,Nivel,CurriculoID")] Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idioma);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Curriculos", new { id = idioma.CurriculoID });
            }            
            return View(idioma);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Shared");
            }

            var idioma = await _context.Idiomas.FindAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var curriculo = await _context.Curriculos
                  .Include(c => c.Usuario)
                  .Where(c => c.UsuarioID == Convert.ToInt32(userId) && c.CurriculoID == idioma.CurriculoID)
                  .FirstOrDefaultAsync();
            if (idioma == null || curriculo == null)
            {
                return RedirectToAction("Error", "Shared");
            }            
            return View(idioma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdiomaID,Nome,Nivel,CurriculoID")] Idioma idioma)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var curriculo = await _context.Curriculos
                  .Include(c => c.Usuario)
                  .Where(c => c.UsuarioID == Convert.ToInt32(userId) && c.CurriculoID == idioma.CurriculoID)
                  .FirstOrDefaultAsync();
            if (id != idioma.IdiomaID || curriculo == null)
            {
                return RedirectToAction("Error", "Shared");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idioma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdiomaExists(idioma.IdiomaID))
                    {
                        return RedirectToAction("Error", "Shared");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculos", new { id = idioma.CurriculoID });
            }
            
            return View(idioma);
        }
       
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var idioma = await _context.Idiomas.FindAsync(id);
            _context.Idiomas.Remove(idioma);
            await _context.SaveChangesAsync();
            return Json("Idioma excluído com sucesso");
        }

        private bool IdiomaExists(int id)
        {
            return _context.Idiomas.Any(e => e.IdiomaID == id);
        }
    }
}
