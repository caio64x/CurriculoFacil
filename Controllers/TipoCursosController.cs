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
    public class TipoCursosController : Controller
    {
        private readonly Contexto _context;

        public TipoCursosController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoCursos
        public async Task<IActionResult> Index( )
        {
            return View(await _context.TipoCursos.ToListAsync());
        }

        // GET: TipoCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCurso = await _context.TipoCursos
                .FirstOrDefaultAsync(m => m.TipoCursoID == id);
            if (tipoCurso == null)
            {
                return NotFound();
            }

            return View(tipoCurso);
        }

        // GET: TipoCursos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoCursoID,Tipo")] TipoCurso tipoCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCurso);
        }

        // GET: TipoCursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCurso = await _context.TipoCursos.FindAsync(id);
            if (tipoCurso == null)
            {
                return NotFound();
            }
            return View(tipoCurso);
        }

        // POST: TipoCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoCursoID,Tipo")] TipoCurso tipoCurso)
        {
            if (id != tipoCurso.TipoCursoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCursoExists(tipoCurso.TipoCursoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCurso);
        }



        // POST: TipoCursos/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int ID)
        {
            var tipoCurso = await _context.TipoCursos.FindAsync(ID);
            _context.TipoCursos.Remove(tipoCurso);
            await _context.SaveChangesAsync();
            return Json(tipoCurso.Tipo + "excluido com sucesso");
        }

        private bool TipoCursoExists(int ID)
        {
            return _context.TipoCursos.Any(e => e.TipoCursoID == ID);
        }
    }
}
