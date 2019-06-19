using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Eve_Intel_Manager;
using EVEStandard;
using Eve_Intel_Manager.Views.Secure;
using Eve_Intel_Manager.Controllers;
using Eve_Intel_Manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EVEStandard.Models.API;
using EVEStandard.Models.SSO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eve_Intel_Manager.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eve_Intel_Manager.Models
{
    [Authorize][Table("AccessList")]
    public class AccessModel

    {
        [Key][Required]
        public int corpID { get; set; }
        [Required]
        public string corpName { get; set; }

        
        
    }
}