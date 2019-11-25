using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brander.Data;
using Brander.Models;
using Brander.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brander.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SupplierController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Supplier.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            //Primero veremos si lo que se puso en el formulario es valido
            if (ModelState.IsValid)
            {
                //si es valido deberia añadir el objeto a la tabla
                _db.Add(supplier);
                //realizando los cambios a la base de datos
                await _db.SaveChangesAsync();

                //redirigir al index despues de completar la operacion
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        //GET  - EDIT
        //int? es lo mismo que Nullable, permite tener valores "nulos" en un int
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _db.Supplier.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);

        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //Actualizaremos por primary key, como solo se actualizara un objeto a la vez esto no puede ser considerado ineficiente, no obstante existen formas mas eficientes que esta
                _db.Update(supplier);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }


        //GET  - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _db.Supplier.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);

        }


        //aca le cambiamos el nombre al metodo porque habria 2 metodos que recibierian un int que se llamarian aiguales, no obstante para que ASp igual lo reconosca como un DLETE
        //le usimos en el ActionName un Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var supplier = await _db.Supplier.FindAsync(id);

            if (id == null)
            {
                return View();
            }

            _db.Supplier.Remove(supplier);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET-DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _db.Supplier.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);

        }


    }
}