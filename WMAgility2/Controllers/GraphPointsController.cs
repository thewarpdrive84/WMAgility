using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WMAgility2.Data;
using WMAgility2.Models;

namespace WMAgility2.Controllers
{
    public class GraphPointsController : Controller
    {
        private readonly ApplicationDbContext _db;

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };


        public GraphPointsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<GraphPoint> objList = _db.GraphPoints;
            return View(objList);
        }

        public IActionResult Create()
        {
            ViewData["x"] = new SelectList(_db.GraphPoints, "x", "y");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("x,y")] GraphPoint point)
        {
            if (ModelState.IsValid)
            {
                _db.Add(point);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["x"] = new SelectList(_db.GraphPoints, "x", "y", point.x);
            return View(point);
        }

        // GET: HowTo
        public ActionResult DataFromDataBase()
        {
            ViewBag.DataPoints = JsonConvert.SerializeObject(_db.GraphPoints.ToList(), _jsonSetting);
            return View();
        }
    }