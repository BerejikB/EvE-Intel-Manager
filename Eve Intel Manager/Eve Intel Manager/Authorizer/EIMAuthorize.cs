using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EVEStandard;
using EVEStandard.Models.API;
using EVEStandard.Models.SSO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Entities;
using Eve_Intel_Manager.Models;
using System.Security.Claims;


public class EIMAuthorize : AuthorizeAttribute
{
    private readonly bool _authorize;
    private readonly EIMReportsDbContext _context;
    private readonly EVEStandardAPI esiClient;
   
    public EIMAuthorize()
    {
        _authorize = false;
    }

    public bool CheckData(EIMReportsDbContext context, EVEStandardAPI esiClient)
    {

        ////string charname = characterInfo.Model.Name;
        //bool authd = context.UserModel.Any(cn => cn.charName == charname)
        //           && context.UserModel.Any(cr => cr.charRole == "Admin");
        bool authd = false;
        return  authd;
    }




    public EIMAuthorize(bool authorize, EIMReportsDbContext context, EVEStandardAPI esiClient)
    {
        _context = context;
        this.esiClient = esiClient;
        _authorize = CheckData(context, esiClient);
    }

    
}