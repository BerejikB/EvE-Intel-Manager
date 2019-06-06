using System;
using System.Collections.Generic;
using System.Linq;
using EVEStandard.Enumerations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EVEStandard;
using Microsoft.EntityFrameworkCore;
using Eve_Intel_Manager.Entities;

using System.Threading.Tasks;

namespace Eve_Intel_Manager
{



    public class DBInitalize : DbContext
    {
        
        //public void InitalizeDB()
        //{
        //     EIMReportsDbContext db = new EIMReportsDbContext();
            

        //    db.Database.EnsureCreated();

        //}
    }
}


