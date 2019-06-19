using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eve_Intel_Manager;
using EVEStandard;
using Eve_Intel_Manager.Views.Secure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Eve_Intel_Manager.Entities;
using Eve_Intel_Manager.Models;

namespace Eve_Intel_Manager.Entities
{
    public class EIMReportsDbContext : DbContext
    {
        public EIMReportsDbContext(DbContextOptions<EIMReportsDbContext> o)
            : base(o)
        { }


        public DbSet<Reports> Report { get; set; }
        public DbSet<AccessModel> AccessModel { get; set; }
        public DbSet<Eve_Intel_Manager.Models.UserModel> UserModel { get; set; }



        //public DbSet<Eve_Intel_Manager.Entities.Bookmarks> Bookmarks { get; set; }
    }
}
