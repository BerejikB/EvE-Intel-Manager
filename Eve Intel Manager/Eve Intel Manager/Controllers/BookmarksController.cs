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
    public class BookmarksController : Controller
    {
        private readonly EIMBookmarksDbContext _context;
        private readonly EVEStandardAPI esiClient;

        public BookmarksController(EIMBookmarksDbContext context)
        {
            _context = context;
            this.esiClient = esiClient;
        }

        ESIModelDTO<List<EVEStandard.Models.Bookmark>> playerbms = new ESIModelDTO<List<EVEStandard.Models.Bookmark>>();
        
        // GET: Bookmarks
        public async Task<IActionResult> Index()
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

            playerbms = await esiClient.Bookmarks.ListBookmarksV2Async(auth, 1, "ifNoneMatch=null");
            //return View(await _context.Bookmarks.ToListAsync());
            return View(playerbms);
        }

        // GET: Bookmarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarks = await _context.Bookmarks
                .FirstOrDefaultAsync(m => m.BookmarkID == id);
            if (bookmarks == null)
            {
                return NotFound();
            }

            return View(bookmarks);
        }

        // GET: Bookmarks/Create
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

            

            var bookmarks = new Eve_Intel_Manager.Entities.Bookmarks
            {
                
           

            };


            return View(bookmarks);
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookmarkID,Xcoord,Ycoord,Zcoord,Created,CreatorId,FolderId,Label,LocationID,Notes")] Bookmarks bookmarks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookmarks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookmarks);
        }

        // GET: Bookmarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarks = await _context.Bookmarks.FindAsync(id);
            if (bookmarks == null)
            {
                return NotFound();
            }
            return View(bookmarks);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookmarkID,Xcoord,Ycoord,Zcoord,Created,CreatorId,FolderId,Label,LocationID,Notes")] Bookmarks bookmarks)
        {
            if (id != bookmarks.BookmarkID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookmarks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookmarksExists(bookmarks.BookmarkID))
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
            return View(bookmarks);
        }

        // GET: Bookmarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarks = await _context.Bookmarks
                .FirstOrDefaultAsync(m => m.BookmarkID == id);
            if (bookmarks == null)
            {
                return NotFound();
            }

            return View(bookmarks);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookmarks = await _context.Bookmarks.FindAsync(id);
            _context.Bookmarks.Remove(bookmarks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookmarksExists(int id)
        {
            return _context.Bookmarks.Any(e => e.BookmarkID == id);
        }



        public void ReadBmList()
        { }
    }
}
