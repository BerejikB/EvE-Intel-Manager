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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Entities;


namespace Eve_Intel_Manager.Authorizer
{
    public class CharInfo : Controller
    {
        private readonly EVEStandardAPI esiClient;
        private readonly EIMReportsDbContext accessList;
         public CharInfo(EVEStandardAPI esiClient, EIMReportsDbContext accessList)
        {
            this.esiClient = esiClient;
            this.accessList = accessList;
        }
        public string characterName { get; set; }
        public string characterRole { get; set; }
        public string characterCorp { get; set; }
        public string characterSystem { get; set; }
        public async Task SetData()
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
            characterName = characterInfo.Model.Name;
            characterSystem = location.Model.Name;
            //characterRole = accessList.UserModel.characterRole;
            characterRole = corporationInfo.Model.Name;
        }

    }
}