
//public async Task<IActionResult> Index()
//{
//    var characterId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
//    var characterInfo = await esiClient.Character.GetCharacterPublicInfoV4Async(characterId);
//    var corporationInfo = await esiClient.Corporation.GetCorporationInfoV4Async((int)characterInfo.Model.CorporationId);

//    var auth = new AuthDTO
//    {
//        AccessToken = new AccessTokenDetails
//        {
//            AccessToken = User.FindFirstValue("AccessToken"),
//            ExpiresUtc = DateTime.Parse(User.FindFirstValue("AccessTokenExpiry")),
//            RefreshToken = User.FindFirstValue("RefreshToken")
//        },
//        CharacterId = characterId,
//        Scopes = User.FindFirstValue("Scopes")
//    };

//    var locationInfo = await esiClient.Location.GetCharacterLocationV1Async(auth);
//    var location = await esiClient.Universe.GetSolarSystemInfoV4Async(locationInfo.Model.SolarSystemId);
//    string charname = characterInfo.Model.Name;
//    string corpinfo = corporationInfo.Model.Name;
//    await AuthUser(corpinfo, charname);
//    //INSERT MODEL HERE
//    //////

//    if (isAuthed)
//    {
//        return View(model);
//    }

//    var notAuthorized = new ErrorViewModel();
//    return View(notAuthorized);
//}



//public async Task AuthUser(string usercorp, string charname)
//{


//    isAuthed = accessList.UserModel.Any(cn => cn.charName == charname)
//            && accessList.AccessModel.Any(n => n.corpName == usercorp);


//    if (!isAuthed)
//    {
//        accessList.UserModel.Add(new UserModel() { charName = charname, charRole = "User" });
//        accessList.SaveChanges();

//        isAuthed = accessList.AccessModel.Any(n => n.corpName == usercorp);

//    }
//}