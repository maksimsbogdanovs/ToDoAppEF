using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Excteptions;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoListModelsController : BaseController
    {
        private readonly ToDoContext _context;

        public ToDoListModelsController(ToDoContext context)
        {
            _context = context;
        }

        public override IActionResult Index()
        {
            if (_context.ToDoListModel == null)
            {
                return NotFound();
            }

            List<ToDoListModel> toDoList = _context.ToDoListModel.Where(m => m.IsDone == false).ToList<ToDoListModel>();

            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // POST: ToDoListModels/Done
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Done(int? id)
        {
            if (id == null || _context.ToDoListModel == null)
            {
                return NotFound();
            }

            var toDoList = _context.ToDoListModel.Where(m => m.ToDoId == id).FirstOrDefault();

            if (toDoList == null)
            {
                return NotFound();
            }

            toDoList.IsDone = true;

            ILogger historyFileLogger = new HistoryFileLogger();
            CheckCanLog(historyFileLogger);

            void CheckCanLog(ILogger logger)
            {
                DateTime dateNow = DateTime.Now;
                string line = $"done : {toDoList.ToDoId} - {toDoList.ToDoListItem} - {dateNow}";
                logger.Log(line);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));        
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(int id, bool isDone)
        //{
        //    var item = await _context.ToDoListModel.FirstOrDefaultAsync(i => i.ToDoId == id);
        //    if (item != null)
        //    {
        //        item.IsDone = isDone;
        //        _context.Update(item);
        //        await _context.SaveChangesAsync();
        //    }

        //    return Ok();
        //}

        // GET: ToDoListModels
        //public async Task<IActionResult> Index()
        //{
        //      return _context.ToDoListModel != null ? 
        //                  View(await _context.ToDoListModel.ToListAsync()) :
        //                  Problem("Entity set 'ToDoContext.ToDoListModel'  is null.");
        //}

        // GET: ToDoListModels/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoListModel == null)
            {
                return NotFound();
            }

            // var toDoListModel = await _context.ToDoListModel.FirstOrDefaultAsync(m => m.ToDoId == id);

            return _context.ToDoListModel != null ? 
                View(await _context.ToDoListModel.FirstOrDefaultAsync(m => m.ToDoId == id)) : 
                    NotFound();

        }*/

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
        public async Task<IActionResult> Create([Bind("ToDoId,ToDoListItem,Created,Urgencylevel,DueDate,IsDone")] ToDoListModel toDoListModel)
        {
            if (string.IsNullOrEmpty(toDoListModel.ToDoListItem))
            {
                throw new NotValidTextException("Lietotājs nav ievadījis tekstu");
            }

            if (toDoListModel.DueDate <= DateTime.Now)
            {
                throw new NotValidDateException("Lietotājs norādījis atpakaļejošu datumu");
            }

            if (ModelState.IsValid)
            {
                _context.Add(toDoListModel);
                await _context.SaveChangesAsync();

                ILogger historyFileLogger = new HistoryFileLogger();
                CheckCanLog(historyFileLogger);

                void CheckCanLog(ILogger logger)
                {
                    string line = $"insert : {toDoListModel.ToDoId} - {toDoListModel.ToDoListItem} - {toDoListModel.Created}"; 
                    logger.Log(line); 
                }

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

            if (string.IsNullOrEmpty(toDoListModel.ToDoListItem))
            {
                throw new NotValidTextException("Lietotājs nav ievadījis tekstu");
            }

            if (toDoListModel.DueDate <= DateTime.Now)
            {
                throw new NotValidDateException("Lietotājs norādījis atpakaļejošu datumu");
            }

            return View(toDoListModel);
        }

        // POST: ToDoListModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoId,ToDoListItem,Created,Urgencylevel,DueDate,IsDone")] ToDoListModel toDoListModel)
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

                    ILogger historyFileLogger = new HistoryFileLogger();
                    CheckCanLog(historyFileLogger);

                    void CheckCanLog(ILogger logger)
                    {
                        DateTime dateNow = DateTime.Now;
                        string line = $"edit : {toDoListModel.ToDoId} - {toDoListModel.ToDoListItem} - {dateNow}";
                        logger.Log(line);
                    }
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

            var toDoListModel = await _context.ToDoListModel.FirstOrDefaultAsync(m => m.ToDoId == id);

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
                return NotFound();
            }

            var toDoListModel = await _context.ToDoListModel.FindAsync(id);

            if (toDoListModel == null)
            {
                return NotFound();
            }

            _context.ToDoListModel.Remove(toDoListModel);

            ILogger historyFileLogger = new HistoryFileLogger();
            CheckCanLog(historyFileLogger);

            void CheckCanLog(ILogger logger)
            {
                DateTime dateNow = DateTime.Now;
                string line = $"delete : {toDoListModel.ToDoId} - {toDoListModel.ToDoListItem} - {dateNow}";
                logger.Log(line);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListModelExists(int id)
        {
            return (_context.ToDoListModel?.Any(e => e.ToDoId == id)).GetValueOrDefault();
        }

        public IActionResult History()
        {
            if (_context.ToDoListModel == null)
            {
                return NotFound();
            }

            List<ToDoListModel> toDoList = _context.ToDoListModel.Where(m => m.IsDone == true).ToList<ToDoListModel>();

            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: ToDoListModels
        public IActionResult AllToDos()
        {
            if (_context.ToDoListModel == null)
            {
                return NotFound();
            }

            List<ToDoListModel> toDoList = _context.ToDoListModel.ToList();

            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }
    }
}
