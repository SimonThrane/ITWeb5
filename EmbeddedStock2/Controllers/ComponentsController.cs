﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock2.Models;

namespace EmbeddedStock2.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly EmbeddedStock2Context _context;

        public ComponentsController(EmbeddedStock2Context context)
        {
            _context = context;
        }

        // GET: Components
        public async Task<IActionResult> Index(int componentTypeId)
        {
            ViewBag.ComponentTypes = _context.ComponentTypes
                .Select(c => new SelectListItem() { Text = c.ComponentName, Value = c.ComponentTypeId.ToString() })
                .ToList();
            var component = _context.Components.AsQueryable();
            if (componentTypeId != 0)
            {
                component = component.Where(c => c.ComponentTypeId == componentTypeId);
            }
            return View(await component.AsNoTracking().ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .SingleOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            var componentTypes = _context.ComponentTypes.Select(c => new SelectListItem()
            {
                Text = c.ComponentName,
                Value = c.ComponentTypeId.ToString()

            });
            if (componentTypes.Count() > 0)
            {
                ViewBag.ComponentTypes = componentTypes;
                return View();
            }
            return View("NoComponentTypes");
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,ComponentTypeId,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] Component component)
        {
            if (ModelState.IsValid)
            {
                _context.Add(component);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(component);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.SingleOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }
            ViewBag.ComponentTypes = _context.ComponentTypes.Select(c => new SelectListItem()
            {
                Text = c.ComponentName,
                Value = c.ComponentTypeId.ToString()

            });
            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentId,ComponentTypeId,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] Component component)
        {
            if (id != component.ComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.ComponentId))
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
            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .SingleOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var component = await _context.Components.SingleOrDefaultAsync(m => m.ComponentId == id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(long id)
        {
            return _context.Components.Any(e => e.ComponentId == id);
        }
    }
}
