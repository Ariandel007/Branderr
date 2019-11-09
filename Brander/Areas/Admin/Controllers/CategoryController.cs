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
    [Area("Admin")]//ahora el controlador es alcanzable
    [Authorize(Roles = SD.ManagerUser)]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;//aplicaremos inyeccion de dependencias

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        //GET action method, con esto vamos a listar las categorias
        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.ToListAsync());
        }

        //GET- CREATE Aqui no usaremos un async porque solo retornaremos un view
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        //Cada vez que creamos un metodo post debemos indicar que es un metodo httpPost y poner depsues el ValidateAntiForgeryToken

        /*ValidateAntiForgeryToken de MVC escribe un valor único en una cookie solo HTTP y luego se escribe el mismo valor en el 
        formulario. Cuando se envía la página, se genera un error si el valor de la cookie no coincide con el valor del formulario.*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            //Primero veremos si lo que se puso en el formulario es valido
            if (ModelState.IsValid)
            {
                //si es valido deberia añadir el objeto a la tabla
                _db.Add(category);
                //realizando los cambios a la base de datos
                await _db.SaveChangesAsync();

                //redirigir al index despues de completar la operacion
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET  - EDIT
        //int? es lo mismo que Nullable, permite tener valores "nulos" en un int
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //Actualizaremos por primary key, como solo se actualizara un objeto a la vez esto no puede ser considerado ineficiente, no obstante existen formas mas eficientes que esta
                _db.Update(category);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }


        //GET  - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }


        //aca le cambiamos el nombre al metodo porque habria 2 metodos que recibierian un int que se llamarian aiguales, no obstante para que ASp igual lo reconosca como un DLETE
        //le usimos en el ActionName un Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var category = await _db.Category.FindAsync(id);

            if (id == null)
            {
                return View();
            }

            _db.Category.Remove(category);
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
            var category = await _db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }

    }
}