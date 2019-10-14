using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Brander.Data;
using Brander.Models.ViewModels;
using Brander.Utility;


namespace Brander.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _db;
        //tambien tendremos que guarsar imagenes en el servidor, por lo que tendremos que traerlas usanod inyeccion de depenecnias
        private readonly IHostingEnvironment _hostingEnvironment;

        [BindProperty]//elanzamos esta  propiedad a otras, para no tener que llamar al viewmodel en la vista
        public GameViewModel GameVM { get; set; }

        public GameController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

            GameVM = new GameViewModel()
            {
                Category = _db.Category,
                //las subcatgeorias dependeran de la categoria que elijamos
                Game = new Models.Game()
            };
        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _db.Game.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
            return View(menuItems);
        }

        //GET-Create
        public IActionResult Create()
        {
            return View(GameVM);
        }

        //POST-Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            GameVM.Game.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(GameVM);
            }

            _db.Game.Add(GameVM.Game);

            await _db.SaveChangesAsync();

            //trabajar en la seccion de guardado de imagenes
            //extraremos la carpeta razi de la aplicacion y para eso necesitaremos el hostingeviroment que tenemos en el DI
            string webRootPath = _hostingEnvironment.WebRootPath;

            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.Game.FindAsync(GameVM.Game.Id);

            if (files.Count > 0)
            {
                //archivos subidos
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, GameVM.Game.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                menuItemFromDb.Image = @"\images\" + GameVM.Game.Id + extension;
            }
            else
            {
                //np hay archivos subidos
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultGameImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + GameVM.Game.Id + ".png");
                menuItemFromDb.Image = @"\images\" + GameVM.Game.Id + ".png";

            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameVM.Game = await _db.Game.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
            GameVM.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == GameVM.Game.CategoryId).ToListAsync();

            if (GameVM.Game == null)
            {
                return NotFound();
            }
            return View(GameVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GameVM.Game.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                GameVM.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == GameVM.Game.CategoryId).ToListAsync();
                return View(GameVM);
            }

            //guardado de imagen

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.Game.FindAsync(GameVM.Game.Id);

            if (files.Count > 0)
            {
                //nuevo imagen ha sido subida
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, menuItemFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //subiremos el nuevo archivo
                using (var filesStream = new FileStream(Path.Combine(uploads, GameVM.Game.Id + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.Image = @"\images\" + GameVM.Game.Id + extension_new;
            }

            menuItemFromDb.Name = GameVM.Game.Name;
            menuItemFromDb.Description = GameVM.Game.Description;
            menuItemFromDb.CategoryId = GameVM.Game.CategoryId;
            menuItemFromDb.SubCategoryId = GameVM.Game.SubCategoryId;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET : Details Game
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameVM.Game = await _db.Game.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);

            if (GameVM.Game == null)
            {
                return NotFound();
            }

            return View(GameVM);
        }

        //GET : Delete Game
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameVM.Game = await _db.Game.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);

            if (GameVM.Game == null)
            {
                return NotFound();
            }

            return View(GameVM);
        }

        //POST Delete Game
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Models.Game menuItem = await _db.Game.FindAsync(id);

            if (menuItem != null)
            {
                var imagePath = Path.Combine(webRootPath, menuItem.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.Game.Remove(menuItem);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }
    }
}