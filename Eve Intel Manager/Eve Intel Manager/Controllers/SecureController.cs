
using EVEStandard.Models.API;
using EVEStandard.Models.SSO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EVEStandard;
using Eve_Intel_Manager.Models;

using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Entities;


namespace Eve_Intel_Manager.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        private readonly EVEStandardAPI esiClient;
        private readonly EIMReportsDbContext accessList;

        public bool isAuthed = false;
        public bool isAdmin = false;
        public SecureController(EVEStandardAPI esiClient, EIMReportsDbContext accessList)
        {
            this.esiClient = esiClient;
            this.accessList = accessList;
        }

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

            var locationInfo = await esiClient.Location.GetCharacterLocationV1Async(auth);
            var location = await esiClient.Universe.GetSolarSystemInfoV4Async(locationInfo.Model.SolarSystemId);
            string charname = characterInfo.Model.Name;
            string corpinfo = corporationInfo.Model.Name;
            await AuthUser(corpinfo, charname);


            var model = new Eve_Intel_Manager.Models.SecurePageViewModel
            {
                

            CharacterName = characterInfo.Model.Name,
                CorporationName = corporationInfo.Model.Name,
                CharacterLocation = location.Model.Name,
            };
            if (isAdmin)
            {
                model.isAdmin = true;
            }

            if (isAuthed)
            {
                return View(model);
            }

            var notAuthorized = new ErrorViewModel();
            return View(notAuthorized);
        }



        public async Task AuthUser(string usercorp, string charname)
        {


            isAuthed = accessList.UserModel.Any(cn => cn.charName == charname)
                    && accessList.AccessModel.Any(n => n.corpName == usercorp);
            isAdmin = accessList.UserModel.Any(cr => cr.charRole == "Admin" && cr.charName == charname);

            
            if (!isAuthed)
            {
                accessList.UserModel.Add(new UserModel() { charName = charname, charRole = "User" });
                accessList.SaveChanges();

                isAuthed = accessList.AccessModel.Any(n => n.corpName == usercorp);

            }
           
        }
    }
}