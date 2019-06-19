using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Models;
using Eve_Intel_Manager.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Eve_Intel_Manager.Controllers
{
    [Authorize]
    public class AccessController : Controller
    {
        private readonly EIMReportsDbContext _context;

        public AccessController(EIMReportsDbContext context)
        {
            _context = context;
        }

        // GET: Access
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccessModel.ToListAsync());
        }

        // GET: Access/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessModel = await _context.AccessModel
                .FirstOrDefaultAsync(m => m.corpID == id);
            if (accessModel == null)
            {
                return NotFound();
            }

            return View(accessModel);
        }

        // GET: Access/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Access/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("corpID,corpName")] AccessModel accessModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessModel);
        }

        // GET: Access/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessModel = await _context.AccessModel.FindAsync(id);
            if (accessModel == null)
            {
                return NotFound();
            }
            return View(accessModel);
        }

        // POST: Access/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("corpID,corpName")] AccessModel accessModel)
        {
            if (id != accessModel.corpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessModelExists(accessModel.corpID))
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
            return View(accessModel);
        }

        // GET: Access/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessModel = await _context.AccessModel
                .FirstOrDefaultAsync(m => m.corpID == id);
            if (accessModel == null)
            {
                return NotFound();
            }

            return View(accessModel);
        }

        // POST: Access/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessModel = await _context.AccessModel.FindAsync(id);
            _context.AccessModel.Remove(accessModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessModelExists(int id)
        {
            return _context.AccessModel.Any(e => e.corpID == id);
        }
    }
}
