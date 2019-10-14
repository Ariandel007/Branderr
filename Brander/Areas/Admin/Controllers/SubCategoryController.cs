using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brander.Data;
using Brander.Models;
using Brander.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Brander.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;//aplicaremos inyeccion de dependencias

        [TempData]//valor que se alm,acenara una vez y desues de borrara
        public string StatusMessage { get; set; }


        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get-index
        public async Task<IActionResult> Index()
        {
            //tambien queremos traer las categorias, para eso el Include
            var subCategories = await _db.SubCategory.Include(s => s.Category).ToListAsync();
            return View(subCategories);
        }

        //GET-Create
        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);

        }

        //POST_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                //esto recupera todos los  regsitros en los que el nombre sea el mismo
                var doesSubCategioryExists = _db.SubCategory.Include(s => s.Category).Where(s => s.Name == model.SubCategory.Name && s.Category.Id == model.SubCategory.CategoryId);

                if (doesSubCategioryExists.Count() > 0)
                {
                    //error
                    StatusMessage = "Error: La categoria existe bajo la categoria: " + doesSubCategioryExists.First().Category.Name + ". Por favor, use otro nombre";
                }
                else
                {
                    _db.SubCategory.Add(model.SubCategory);

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }

            //en caso de que no sea valido tendremos que retroceder y crear el view model
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
                //Idneitty ya tiene un statusmessage que podemos usar
            };

            return View(modelVM);
        }

        //darle un nombre a la accion, esto es para la lista de subcategorias ya existentes ya que no podemos llamarla
        [ActionName("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            subCategories = await (from SubCategory in _db.SubCategory
                                   where SubCategory.CategoryId == id
                                   select SubCategory).ToListAsync();

            return Json(new SelectList(subCategories, "Id", "Name"));
        }



        //GET-Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _db.SubCategory.SingleOrDefaultAsync(m => m.Id == id);

            if (subCategory == null)
            {
                return NotFound();
            }

            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = subCategory,
                SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);

        }

        //POST-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                //esto recupera todos los  regsitros en los que el nombre sea el mismo
                var doesSubCategioryExists = _db.SubCategory.Include(s => s.Category).Where(s => s.Name == model.SubCategory.Name && s.Category.Id == model.SubCategory.CategoryId);

                if (doesSubCategioryExists.Count() > 0)
                {
                    //error
                    StatusMessage = "Error: La categroia existe bajo la categoria: " + doesSubCategioryExists.First().Category.Name + ". Por favor, use otro nombre";
                }
                else
                {
                    //actualizar solo a la categoria elegida
                    var subCatFromDb = await _db.SubCategory.FindAsync(id);
                    subCatFromDb.Name = model.SubCategory.Name;

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }

            //en caso de que no sea valido tendremos que retroceder y crear el view model
            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
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
            var subCategory = await _db.SubCategory.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await _db.SubCategory.Include(s => s.Category).SingleOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _db.SubCategory.SingleOrDefaultAsync(m => m.Id == id);
            _db.SubCategory.Remove(subCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}