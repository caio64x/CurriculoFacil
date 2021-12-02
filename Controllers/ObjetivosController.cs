using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using MontagemCurriculo.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MontagemCurriculo.Controllers
{
    [Authorize]
    public class ObjetivosController : Controller
    {
        private readonly Contexto _context;
        
        public ObjetivosController(Contexto context)
        {
            _context = context;
        }


            

        // GET: Objetivos/Create
        public IActionResult Create(int  id)
        {
            if(User.Identity.IsAuthenticated){
                Objetivo objetivo = new Objetivo();
                objetivo.CurriculoID = id;
                return View(objetivo);
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetivoID,Descrição,CurriculoID")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objetivo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","Curriculos", new { id = objetivo.CurriculoID });
            }
            return View(objetivo);
        }

        // GET: Objetivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Shared");
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var objetivo = await _context.Objetivos.FindAsync(id);

            var curriculo = await _context.Curriculos
                   .Include(c => c.Usuario)
                   .Where(c => c.UsuarioID == Convert.ToInt32(userId) && c.CurriculoID == objetivo.CurriculoID)
                   .FirstOrDefaultAsync();

        //   objetivo.CurriculoID

            if (objetivo == null || curriculo == null)
            {
                return RedirectToAction("Error", "Shared");
            }


            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetivoID,Descrição,CurriculoID")] Objetivo objetivo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var curriculo = await _context.Curriculos
                  .Include(c => c.Usuario)
                  .Where(c => c.UsuarioID == Convert.ToInt32(userId) && c.CurriculoID == objetivo.CurriculoID)
                  .FirstOrDefaultAsync();

            if (id != objetivo.ObjetivoID || curriculo == null)
            {
                return RedirectToAction("Error", "Shared");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objetivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjetivoExists(objetivo.ObjetivoID))
                    {
                        return RedirectToAction("Error", "Shared");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculos", new { id = objetivo.CurriculoID });
            }
            return View(objetivo);
        }

        // POST: Objetivos/Delete/5
        [HttpPost]
        
        public async Task<JsonResult> Delete(int id)
        {
            var objetivo = await _context.Objetivos.FindAsync(id);
            _context.Objetivos.Remove(objetivo);
            await _context.SaveChangesAsync();
            return Json("Objetivo excluído");
        }

        private bool ObjetivoExists(int id)
        {
            return _context.Objetivos.Any(e => e.ObjetivoID == id);
        }
    }
}
