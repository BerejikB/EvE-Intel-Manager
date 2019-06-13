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





namespace Eve_Intel_Manager.Entities
{
    [Authorize]
    public class Bookmarks
    {
        private readonly EVEStandardAPI esiClient;

        [Key]
        [Required]
        public int BookmarkID { get; set; }
        //title: get_characters_character_id_bookmarks_bookmark_id
        [Required]
        public double Xcoord { get; set; }
        [Required]
        public double Ycoord { get; set; }
        [Required]
        public double Zcoord { get; set; }
        [Required]
        public string Created { get; set; }
        //Date/time when created
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public int FolderId { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public int LocationID { get; set; }
        [Required]
        public string Notes { get; set; }
       
    }

}
