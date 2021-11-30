using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;
using MontagemCurriculo.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rotativa.AspNetCore;
using Rotativa;

namespace MontagemCurriculo.Controllers
{
    [Authorize]
    public class CurriculosController : Controller
    {
        private readonly Contexto _context;
     
        public CurriculosController(Contexto context)
        {
            _context = context;
        }

        // GET: Curriculos
        public async Task<IActionResult> Index(int id)
        {
             var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var contexto = _context.Curriculos
                .Include(c => c.Usuario)
                .Where(c => c.Usuario.UsuarioID == Convert.ToInt32(userId));
                                    
            return View(await contexto.ToListAsync());
        }

        // GET: Lista de Curriculos
        [AllowAnonymous]
        public async Task<IActionResult> IndexPublic()
        {
            var contexto = _context.Curriculos
                .Include(u => u.Usuario)
                .Where(c => c.Principal == true)
                .Where(c => c.CurriculoID > 0);

            return View(await contexto.ToListAsync());
        }

        // GET: Curriculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // GET: Curriculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curriculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoID,Nome,UsuarioID")] Curriculo curriculo)
        {
            try
            {
                curriculo.UsuarioID = int.Parse(HttpContext.Session.GetInt32("UsuarioID").ToString());
                if (ModelState.IsValid)
                {
                    _context.Add(curriculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(curriculo);
            }
            catch
            {

                return RedirectToAction("Error", "Shared");
            }
           
        }

        // GET: Curriculos/Edit/5
  public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }           
            return View(curriculo);
        }

        // POST: Curriculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoID,Nome,UsuarioID")] Curriculo curriculo)
        {
            curriculo.UsuarioID = int.Parse(HttpContext.Session.GetInt32("UsuarioID").ToString());
            if (id != curriculo.CurriculoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculoExists(curriculo.CurriculoID))
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
            return View(curriculo);
        }

   

        // POST: Curriculos/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int ID)
        {
            var curriculo = await _context.Curriculos.FindAsync(ID);
            _context.Curriculos.Remove(curriculo);
            await _context.SaveChangesAsync();
            return Json(curriculo.Nome + "excluido com sucesso");
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarPrincipal(int ID)
        {
            var curriculo = await _context.Curriculos.FindAsync(ID);
            if (curriculo.Principal)
            {
                curriculo.Principal = false;
                _context.Curriculos.Update(curriculo);
                await _context.SaveChangesAsync();

            }
            else
            {
                curriculo.Principal = true;
                _context.Curriculos.Update(curriculo);
                await _context.SaveChangesAsync();

            }

            return Json(curriculo.Nome + "Atualizado com sucesso");
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculos.Any(e => e.CurriculoID == id);
        }

        [AllowAnonymous]
        public ActionResult PDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Curriculo curriculo = _context.Curriculos
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CurriculoID == id).Result;
            if (curriculo == null)
            {
                return NotFound();
            }

            
            var idUsuario = HttpContext.Session.GetInt32("UsuarioID");

            CurriculoViewModel curriculoV = new CurriculoViewModel();
            curriculoV.Objetivos = _context.Objetivos.Where(o => o.CurriculoID == curriculo.CurriculoID).ToList();
            curriculoV.FormacoesAcademicas = _context.FormacoesAcademicas.Include(fa => fa.TipoCurso).Where(fa => fa.Curriculo.CurriculoID == curriculo.CurriculoID).ToList();
            curriculoV.ExperienciasProfissionais = _context.ExperienciasProfissionais.Where(ep => ep.Curriculo.CurriculoID == curriculo.CurriculoID).ToList();
            curriculoV.Idiomas = _context.Idiomas.Where(i => i.Curriculo.CurriculoID == curriculo.CurriculoID).ToList();

           // return new ViewAsPdf(curriculoV);
            return  new ViewAsPdf("PDF", curriculoV) { FileName = "Portfolio.pdf" };
            
        }

        [AllowAnonymous]
        public async Task<IActionResult> DetailsPublic(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                               .Where(c => c.Principal == true)
                               .FirstOrDefaultAsync(c => c.CurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
                      
        }
   
        [AllowAnonymous]
        public async Task<JsonResult> Objetivos()
        {
            List<Objetivo> Objetivos = new List<Objetivo>();
            Objetivos = _context.Objetivos.Where(o => o.CurriculoID == 8).ToList();
            return Json(Objetivos);
       
        }

        [AllowAnonymous]
        public async Task<JsonResult> FormacoesAcademicas()
        {
            List<FormacaoAcademica> FormacoesAcademicas = new List<FormacaoAcademica>();
            FormacoesAcademicas = _context.FormacoesAcademicas.Where(fa => fa.Curriculo.CurriculoID == 8).ToList();
            return Json(FormacoesAcademicas);

        }

        [AllowAnonymous]
        public async Task<JsonResult> ExperienciaProfissionais()
        {
            List<ExperienciaProfissional> ExperienciasProfissionais = new List<ExperienciaProfissional>();
            ExperienciasProfissionais = _context.ExperienciasProfissionais.Where(ep => ep.Curriculo.CurriculoID == 8).ToList();
            return Json(ExperienciasProfissionais);

        }

        [AllowAnonymous]
        public async Task<JsonResult> Idiomas()
        {
            List<Idioma> Idiomas = new List<Idioma>();
            Idiomas = _context.Idiomas.Where(i => i.Curriculo.CurriculoID == 8).ToList();
            return Json(Idiomas);

        }

        [AllowAnonymous]
        public async Task<JsonResult> Listagem()
        {


            CurriculoViewModel curriculoV = new CurriculoViewModel();
            curriculoV.Objetivos = _context.Objetivos.Where(o => o.CurriculoID == 8).ToList();
            curriculoV.FormacoesAcademicas = _context.FormacoesAcademicas.Where(fa => fa.Curriculo.CurriculoID == 8).ToList();
            curriculoV.ExperienciasProfissionais = _context.ExperienciasProfissionais.Where(ep => ep.Curriculo.CurriculoID == 8).ToList();
            curriculoV.Idiomas = _context.Idiomas.Where(i => i.Curriculo.CurriculoID == 8).ToList();
            return Json(curriculoV);

        }



        public JsonResult NomeExiste(string Nome)
        {
            if (!_context.Curriculos.Any(e => e.Nome == Nome))
            {
                return Json(true);
            }
            else
            {
                return Json("Nome existente");
            }

        }
    }
}
