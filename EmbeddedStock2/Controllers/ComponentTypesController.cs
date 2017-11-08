using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock2.Models;
using EmbeddedStock2.ViewModels;
using FreeImageAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace EmbeddedStock2.Controllers
{
    public class ComponentTypesController : Controller
    {
        private readonly EmbeddedStock2Context _context;

        public ComponentTypesController(EmbeddedStock2Context context)
        {
            _context = context;
        }

        // GET: ComponentTypes
        public async Task<IActionResult> Index(int categoryId)
        {
            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.CategoryId.ToString() })
                .ToList();
            var componentTypes = _context.ComponentTypes.AsQueryable();
            if (categoryId != 0)
            {
                componentTypes = componentTypes.Where(c => c.ComponentTypeCategory.Any(o => o.CategoryId == categoryId));
            }
            return View(await componentTypes.AsNoTracking().Include(ct => ct.Image).ToListAsync());
        }

        // GET: ComponentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .Include(ct => ct.Image)
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);


            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentTypes/Create
        public IActionResult Create()
        {
            var categories = _context.Categories
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.CategoryId.ToString() })
                .ToList();
            if (categories.Count > 0)
            {
                ViewData["categories"] = categories;
                return View();
            }
            return View("NoCategories");
        }

        // POST: ComponentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateComponentTypeViewModel componentTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                //Make image
                byte[] image;
                byte[] thumbnail;
                using (var m = new MemoryStream())
                {
                    await componentTypeViewModel.ImageUpload.CopyToAsync(m);
                    image = m.ToArray();
                }
                //Make thumbnail
                using (var m = new MemoryStream())
                {
                    await componentTypeViewModel.ImageUpload.CopyToAsync(m);
                    using (var ti = new FreeImageBitmap(componentTypeViewModel.ImageUpload.OpenReadStream()))
                    {
                        var thumb = ti.GetThumbnailImage(100, true);
                        using (var nm = new MemoryStream())
                        {
                            thumb.Save(nm,Util.Util.FindImageFormat(componentTypeViewModel.ImageUpload.ContentType));
                            thumbnail = nm.ToArray();
                        }
                    }
                } 
                    var componentType = new ComponentType
                    {
                        ComponentName = componentTypeViewModel.ComponentName,
                        AdminComment = componentTypeViewModel.AdminComment,
                        ComponentInfo = componentTypeViewModel.ComponentInfo,
                        Datasheet = componentTypeViewModel.Datasheet,
                        Location = componentTypeViewModel.Location,
                        WikiLink = componentTypeViewModel.WikiLink,
                        Manufacturer = componentTypeViewModel.Manufacturer,
                        Image = new EsImage
                        {
                            ImageData = image,
                            Thumbnail = thumbnail,
                            ImageMimeType = componentTypeViewModel.ImageUpload.ContentType
                        }
                    };
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == componentTypeViewModel.Category);
                if (category != null)
                {
                    _context.Add(componentType);
                    await _context.SaveChangesAsync();

                    _context.Add(new ComponentTypeCategory { Category = category, ComponentType = componentType });
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(componentTypeViewModel);
        }

        // GET: ComponentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes.Include(c => c.Image).SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            this.ViewData["categories"] = _context.Categories
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.CategoryId.ToString() })
                .ToList();
            if (componentType == null)
            {
                return NotFound();
            }
            return View(componentType);
        }

        // POST: ComponentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ComponentType componentType, IFormFile imageFile)
        {

            if (id != componentType.ComponentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile.Length > 0)
                    {
                        var stream = imageFile.OpenReadStream();
                        var bitmap = FreeImageBitmap.FromStream(stream);
                        var thumbnail = bitmap.GetThumbnailImage(1000, true);

                        using (var m = new MemoryStream())
                        {
                            bitmap.Save(m, format: bitmap.ImageFormat);
                            componentType.Image.ImageData = m.ToArray();
                            
                        }
                        using (var m = new MemoryStream())
                        {
                            thumbnail.Save(m, format: FREE_IMAGE_FORMAT.FIF_JPEG);
                            componentType.Image.Thumbnail = m.ToArray();
                        }
                    }

                    _context.Update(componentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.ComponentTypeId))
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
            return View(componentType);
        }

        // GET: ComponentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            _context.ComponentTypes.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(long id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeId == id);
        }
    }
}
