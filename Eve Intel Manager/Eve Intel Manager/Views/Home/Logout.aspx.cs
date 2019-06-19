using Nancy.Session;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nancy.Authentication.Forms;
using Nancy;
namespace logout
{
    class logout
    {
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            FormsAuthentication.LogOutResponse();
        }
    }
}