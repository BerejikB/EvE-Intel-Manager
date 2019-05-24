using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Eve_Intel_Manager;
using EVEStandard;
using Eve_Intel_Manager.Views.Secure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Eve_Intel_Manager.Entities
{ 
    public class Reports
    {
        public static DateTime UtcNow { get; }
        [Key]
        public int ReportID { get; set;  }
        [Required]
        public string ReportBody { get; set; }
        [Required]
        public string ReportGenerated { get; set; } = UtcNow.ToUniversalTime().ToString();
        [Required]
        public string ReportExpiry { get; set; } = "00:15:00";

    }
}
