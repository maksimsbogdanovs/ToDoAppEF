using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoListModelsController : Controller
    {
        private readonly ToDoContext _context;

        public ToDoListModelsController(ToDoContext context)
        {
            _context = context;
        }

        // GET: ToDoListModels
        public async Task<IActionResult> Index()
        {
              return _context.ToDoListModel != null ? 
                          View(await _context.ToDoListModel.ToListAsync()) :
                          Problem("Entity set 'ToDoContext.ToDoListModel'  is null.");
        }

        // GET: ToDoListModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoListModel == null)
            {
                return NotFound();
            }

            var toDoListModel = await _context.ToDoListModel
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDoListModel == null)
            {
                return NotFound();
            }

            return View(toDoListModel);
        }

        // GET: ToDoListModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoListModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoId,ToDoListItem,Created,Urgencylevel")] ToDoListModel toDoListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoListModel);
        }

        // GET: ToDoListModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDoListModel == null)
            {
                return NotFound();
            }

            var toDoListModel = await _context.ToDoListModel.FindAsync(id);
            if (toDoListModel == null)
            {
                return NotFound();
            }
            return View(toDoListModel);
        }

        // POST: ToDoListModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoId,ToDoListItem,Created,Urgencylevel")] ToDoListModel toDoListModel)
        {
            if (id != toDoListModel.ToDoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListModelExists(toDoListModel.ToDoId))
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
            return View(toDoListModel);
        }

        // GET: ToDoListModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDoListModel == null)
            {
                return NotFound();
            }

            var toDoListModel = await _context.ToDoListModel
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDoListModel == null)
            {
                return NotFound();
            }

            return View(toDoListModel);
        }

        // POST: ToDoListModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDoListModel == null)
            {
                return Problem("Entity set 'ToDoContext.ToDoListModel'  is null.");
            }
            var toDoListModel = await _context.ToDoListModel.FindAsync(id);
            if (toDoListModel != null)
            {
                _context.ToDoListModel.Remove(toDoListModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListModelExists(int id)
        {
          return (_context.ToDoListModel?.Any(e => e.ToDoId == id)).GetValueOrDefault();
        }
    }
}
