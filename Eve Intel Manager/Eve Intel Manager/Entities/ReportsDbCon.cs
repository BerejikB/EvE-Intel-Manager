using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eve_Intel_Manager;
using EVEStandard;
using Eve_Intel_Manager.Views.Secure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eve_Intel_Manager.Entities
{
    public class EIMReportsDbContext : DbContext
    {
        IConfiguration config;

        public EIMReportsDbContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DbConString"));
        }
        
        public DbSet<Reports> Report { get; set; }
    }
}
