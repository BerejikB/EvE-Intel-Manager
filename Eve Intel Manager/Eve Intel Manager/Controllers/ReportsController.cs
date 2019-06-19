using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Entities;
using EVEStandard;
using EVEStandard.Models.API;
using EVEStandard.Models.SSO;
using Eve_Intel_Manager.Models;
using Microsoft.AspNetCore.Authorization;


using System.Security.Claims;
using Eve_Intel_Manager.Authorizer;

namespace Eve_Intel_Manager.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly EIMReportsDbContext _context;
        private readonly CharInfo _charInfo;
        private readonly EVEStandardAPI esiClient;
        public ReportsController(EIMReportsDbContext context, EVEStandardAPI esiClient, CharInfo charInfo)
        {
            _context = context;
            this.esiClient = esiClient;
            _charInfo = charInfo;

        }
        bool isAuthed = false;
        public async Task AuthUser(EIMReportsDbContext context, EVEStandardAPI esiClient)
        {

            isAuthed = context.UserModel.Any(cn => cn.charName == _charInfo.characterName)
                   && context.AccessModel.Any(n => n.corpName == _charInfo.characterCorp);
        }

        // GET: Reports
        [Authorize]
        public async Task<IActionResult> Index()
        {
            await AuthUser(_context, esiClient);
            if (isAuthed)
            {
                return View(await _context.Report.ToListAsync());
            }
            else
            {
                var notAuthorized = new ErrorViewModel();
                return View(notAuthorized);
            }
           
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reports = await _context.Report
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (reports == null)
            {
                return NotFound();
            }

            return View(reports);
        }

        // GET: Reports/Create
        public async Task<IActionResult> Create()
        {


            var reports = new Eve_Intel_Manager.Entities.Reports();
            var reportInputData = new Eve_Intel_Manager.Models.ReportViewModel();
            await SetData(reports);
            return View(reports);
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reports reports)
        {

            await SetData(reports);
            if (ModelState.IsValid)
            {
                _context.Add(reports);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(reports);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var reports = await _context.Report.FindAsync(id);
            await SetData(reports);
            if (reports == null)
            {
                return NotFound();
            }
            
            return View(reports);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportID,ReportBody,ReportGenerated,ReportExpiry,CreatedBy")] Reports reports)
        {
            

            if (id != reports.ReportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    await SetData(reports);
                    _context.Update(reports);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportsExists(reports.ReportID))
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
            return View(reports);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reports = await _context.Report
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (reports == null)
            {
                return NotFound();
            }

            return View(reports);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reports = await _context.Report.FindAsync(id);
            _context.Report.Remove(reports);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportsExists(int id)
        {
            return _context.Report.Any(e => e.ReportID == id);
        }


        string SetTime()
        {
            var timenow = DateTime.UtcNow;
            string timestring = timenow.ToString("HH:mm");
            return timestring;
        }

        public async Task<IActionResult> SetData(Reports reports)
        {
            var characterId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var characterInfo = await esiClient.Character.GetCharacterPublicInfoV4Async(characterId);
            var corporationInfo = await esiClient.Corporation.GetCorporationInfoV4Async((int)characterInfo.Model.CorporationId);

            var auth = new AuthDTO
            {
                AccessToken = new AccessTokenDetails
                {
                    AccessToken = User.FindFirstValue("AccessToken"),
                    ExpiresUtc = DateTime.Parse(User.FindFirstValue("AccessTokenExpiry")),
                    RefreshToken = User.FindFirstValue("RefreshToken")
                },
                CharacterId = characterId,
                Scopes = User.FindFirstValue("Scopes")
            };

           
            var locationInfo = await esiClient.Location.GetCharacterLocationV1Async(auth);
            var location = await esiClient.Universe.GetSolarSystemInfoV4Async(locationInfo.Model.SolarSystemId);
            reports.CreatedBy = characterInfo.Model.Name;
            reports.ReportLocation = location.Model.Name;
            reports.ReportGenerated = SetTime();
            
            return View(reports);
        }
    }
}
