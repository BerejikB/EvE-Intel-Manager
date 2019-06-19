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
    [Table("UserList")]
    public class UserModel

    {
        [Key]
        [Required]
        public int charID { get; set; }
        [Required]
        public string charName { get; set; }
        [Required]
        public string charRole { get; set; }

    }
}