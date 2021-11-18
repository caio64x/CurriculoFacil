using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontagemCurriculo.Controllers
{
    public class SharedController : Controller
    {
        // GET: SharedController
        public ActionResult Error()
        {
            return View();
        }
             
    }
}
