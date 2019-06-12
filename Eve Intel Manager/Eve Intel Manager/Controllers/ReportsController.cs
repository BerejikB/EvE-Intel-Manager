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

using Microsoft.AspNetCore.Authorization;


using System.Security.Claims;


namespace Eve_Intel_Manager.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly EIMReportsDbContext _context;

        private readonly EVEStandardAPI esiClient;
        public ReportsController(EIMReportsDbContext context, EVEStandardAPI esiClient)
        {
            _context = context;
            this.esiClient = esiClient;

        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {

            return View(await _context.Report.ToListAsync());
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

            var reports = new Eve_Intel_Manager.Entities.Reports
            {
                CreatedBy = characterInfo.Model.Name,
                //CorporationName = corporationInfo.Model.Name,
                ReportLocation = location.Model.Name
            };

            return View(reports);
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportID,ReportBody,ReportGenerated,ReportExpiry,CreatedBy")] Reports reports)
        {
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
            if (reports == null)
            {
                return NotFound();
            }
            return View(reports);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
    }
}
