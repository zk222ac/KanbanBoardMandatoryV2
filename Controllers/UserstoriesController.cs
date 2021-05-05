using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanBoardMandatoryV2.Data;
using KanbanBoardMandatoryV2.Models;
using KanbanBoardMandatoryV2.Services;

namespace KanbanBoardMandatoryV2.Controllers
{
    public class UserstoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserstory __userstoryService;


        public UserstoriesController(ApplicationDbContext context, IUserstory userstoryService)
        {
            _context = context;
            __userstoryService = userstoryService;
        }
        

        // GET
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Userstorys.Include(u => u.User)
                .Where(i => i.UserEmail == User.Identity.Name);
            return View(await applicationDbContext.ToListAsync());
        }

  

        // GET: / Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: 
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Estimation,Priority,UserStatus,UserEmail,UserId")] Userstory userstory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userstory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userstory.UserId);
            return View(userstory);
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userstory = await _context.Userstorys.FindAsync(id);
            if (userstory == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userstory.UserId);
            return View(userstory);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Estimation,Priority,UserStatus,UserEmail,UserId")] Userstory userstory)
        {
            if (id != userstory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userstory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserstoryExists(userstory.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userstory.UserId);
            return View(userstory);
        }

        // GET:
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userstory = await _context.Userstorys
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userstory == null)
            {
                return NotFound();
            }

            return View(userstory);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userstory = await _context.Userstorys.FindAsync(id);
            _context.Userstorys.Remove(userstory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserstoryExists(int id)
        {
            return _context.Userstorys.Any(e => e.Id == id);
        }


        [HttpPost]
        public async Task<IActionResult> Forward(int id)
        {
            Userstory story = _context.Userstorys.FirstOrDefault(i => i.Id == id);
            __userstoryService.Forward(story);
            _context.Update(story);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Backward(int id)
        {
            Userstory story = _context.Userstorys.FirstOrDefault(i => i.Id == id);
            __userstoryService.Backward(story);
            _context.Update(story);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
    }
}
