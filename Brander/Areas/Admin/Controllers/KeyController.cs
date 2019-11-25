using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brander.Data;
using Brander.Models;
using Brander.Models.ViewModels;
using Brander.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Brander.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class KeyController : Controller
    {
        private readonly ApplicationDbContext _db;//aplicaremos inyeccion de dependencias

        
        public KeyController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get-index
        public async Task<IActionResult> Index()
        {
            //tambien queremos traer las categorias, para eso el Include
            var menuitems = await _db.Key.Include(s => s.Stock).Include(s=>s.Game).ToListAsync();
            return View(menuitems);
        }

        //GET-Create
        public async Task<IActionResult> Create()
        {
            KeyViewModel model = new KeyViewModel()
            {
                StockList = await _db.Stock.ToListAsync(),
                GameList = await _db.Game.ToListAsync(),
                Key = new Models.Key()      
            };

            return View(model);

        }

        //POST_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KeyViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Key.Add(model.Key);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //en caso de que no sea valido tendremos que retroceder y crear el view model
            KeyViewModel modelVM = new KeyViewModel()
            {
                StockList = await _db.Stock.ToListAsync(),
                GameList = await _db.Game.ToListAsync(),
                Key = model.Key
            };

            return View(modelVM);
        }

     

        //GET-Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var key = await _db.Key.SingleOrDefaultAsync(m => m.Id == id);

            if (key == null)
            {
                return NotFound();
            }

            KeyViewModel model = new KeyViewModel()
            {
                StockList = await _db.Stock.ToListAsync(),
                GameList = await _db.Game.ToListAsync(),
                Key = key             
            };

            return View(model);

        }

        //POST-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KeyViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                    //actualizar solo a la categoria elegida
                    var keyFromDb = await _db.Key.FindAsync(id);
                    keyFromDb.Id = model.Key.Id;

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                

            }

            //en caso de que no sea valido tendremos que retroceder y crear el view model
            KeyViewModel modelVM = new KeyViewModel()
            {
                StockList = await _db.Stock.ToListAsync(),
                GameList = await _db.Game.ToListAsync(),
                Key = model.Key
            };

            return View(modelVM);
        }

        //GET Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var key = await _db.Key.Include(s => s.Stock).Include(s => s.Game).SingleOrDefaultAsync(m=>m.Id == id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var key = await _db.Key.Include(s => s.Stock).Include(s => s.Game).SingleOrDefaultAsync(m => m.Id == id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var key = await _db.Key.SingleOrDefaultAsync(m => m.Id == id);
            _db.Key.Remove(key);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}