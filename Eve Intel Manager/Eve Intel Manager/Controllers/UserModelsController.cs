using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Entities;
using Eve_Intel_Manager.Models;
using EVEStandard.Models.API;
using EVEStandard.Models.SSO;
using Microsoft.AspNetCore.Authorization;
using EVEStandard;
using System.Security.Claims;
using Eve_Intel_Manager.Authorizer;


namespace Eve_Intel_Manager.Controllers
{
    public class UserModelsController : Controller
    {
        private readonly EIMReportsDbContext _context;
        private readonly EVEStandardAPI esiClient;
        private readonly CharInfo _charInfo;

        public bool isAuthed = false;
        public UserModelsController(EIMReportsDbContext context, EVEStandardAPI esiClient, CharInfo charInfo)
        {
            _context = context;
            this.esiClient = esiClient;
            _charInfo = charInfo;
        }
        
       public  async Task AuthUser( EIMReportsDbContext context, EVEStandardAPI esiClient)
        {
           
             isAuthed = context.UserModel.Any(cn => cn.charName == _charInfo.characterName)
                    && context.UserModel.Any(cr => cr.charRole == "Admin");
            
        }

        
        // GET: UserModels
        [Authorize][EIMAuthorize]
        public async Task<IActionResult> Index()
        {

             await AuthUser(_context, esiClient);
            if (isAuthed)
            {
                return View(await _context.UserModel.ToListAsync());
            }
            else
            {
                var notAuthorized = new ErrorViewModel();
                return View(notAuthorized);
            }
            
        }

        // GET: UserModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.charID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: UserModels/Create
        public IActionResult Create()
        {
            return View() ;
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("charID,charName,charRole")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("charID,charName,charRole")] UserModel userModel)
        {
            if (id != userModel.charID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.charID))
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
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.charID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.UserModel.FindAsync(id);
            _context.UserModel.Remove(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
            return _context.UserModel.Any(e => e.charID == id);
        }
    }
}
