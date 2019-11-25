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
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _db;//aplicaremos inyeccion de dependencias

        [TempData]//valor que se alm,acenara una vez y desues de borrara
        public string StatusMessage { get; set; }


        public StockController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get-index
        public async Task<IActionResult> Index()
        {
            //tambien queremos traer las categorias, para eso el Include
            var stocks = await _db.Stock.Include(s => s.Supplier).ToListAsync();
            return View(stocks);
        }

        //GET-Create
        public async Task<IActionResult> Create()
        {
            StockAndSupplierViewModel model = new StockAndSupplierViewModel()
            {
                SupplierList = await _db.Supplier.ToListAsync(),
                Stock = new Models.Stock(),
                StockList = await _db.Stock.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);

        }

        //POST_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockAndSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                //esto recupera todos los  regsitros en los que el nombre sea el mismo
                var doesStockExists = _db.Stock.Include(s => s.Supplier).Where(s => s.Name == model.Stock.Name && s.Supplier.Id == model.Stock.SupplierId);

                if (doesStockExists.Count() > 0)
                {
                    //error
                    StatusMessage = "Error: El stock existe bajo el proveedor: " + doesStockExists.First().Supplier.CompanyName + ". Por favor, use otro nombre";
                }
                else
                {
                    _db.Stock.Add(model.Stock);

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }

            //en caso de que no sea valido tendremos que retroceder y crear el view model
            StockAndSupplierViewModel modelVM = new StockAndSupplierViewModel()
            {
                SupplierList = await _db.Supplier.ToListAsync(),
                Stock = model.Stock,
                StockList = await _db.Stock.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
                //Idneitty ya tiene un statusmessage que podemos usar
            };

            return View(modelVM);
        }

        //darle un nombre a la accion, esto es para la lista de subcategorias ya existentes ya que no podemos llamarla
        [ActionName("GetStock")]
        public async Task<IActionResult> GetStock(int id)
        {
            List<Stock> stocks = new List<Stock>();

            stocks = await (from Stock in _db.Stock
                                   where Stock.SupplierId == id
                                   select Stock).ToListAsync();

            return Json(new SelectList(stocks, "Id", "Name"));
        }



        //GET-Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _db.Stock.SingleOrDefaultAsync(m => m.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            StockAndSupplierViewModel model = new StockAndSupplierViewModel()
            {
                SupplierList = await _db.Supplier.ToListAsync(),
                Stock = stock,
                StockList = await _db.Stock.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);

        }

        //POST-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StockAndSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                //esto recupera todos los  regsitros en los que el nombre sea el mismo
                var doesStockExists = _db.Stock.Include(s => s.Supplier).Where(s => s.Name == model.Stock.Name && s.Supplier.Id == model.Stock.SupplierId);

                if (doesStockExists.Count() > 0)
                {
                    //error
                    StatusMessage = "Error: El stock existe bajo el proveedor: " + doesStockExists.First().Supplier.CompanyName + ". Por favor, use otro nombre";
                }
                else
                {
                    //actualizar solo a la categoria elegida
                    var stockFromDb = await _db.Stock.FindAsync(id);
                    stockFromDb.Name = model.Stock.Name;

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }

            //en caso de que no sea valido tendremos que retroceder y crear el view model
            StockAndSupplierViewModel modelVM = new StockAndSupplierViewModel()
            {
                SupplierList = await _db.Supplier.ToListAsync(),
                Stock = model.Stock,
                StockList = await _db.Stock.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
                //Idneitty ya tiene un statusmessage que podemos usar
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
            var stock = await _db.Stock.Include(s => s.Supplier).SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stock = await _db.Stock.Include(s => s.Supplier).SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _db.Stock.SingleOrDefaultAsync(m => m.Id == id);
            _db.Stock.Remove(stock);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}