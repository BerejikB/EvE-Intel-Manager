using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Eve_Intel_Manager.Models
{    
        public class SecurePageViewModel
    {
            public string CharacterName { get; set; }
            public string CorporationName { get; set; }
            public string CharacterLocation { get; set; }
        }
    }
