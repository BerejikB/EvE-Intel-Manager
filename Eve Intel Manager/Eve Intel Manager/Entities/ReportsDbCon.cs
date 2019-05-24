using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eve_Intel_Manager;
using EVEStandard;
using Eve_Intel_Manager.Views.Secure;
using System.Data.Entity;

namespace Eve_Intel_Manager.Entities
{
    public class EIMReportsDbContext : DbContext
    {
        public EIMReportsDbContext()
         : base("Server=tcp:eveintelmanager20190521111543dbserver.database.windows.net,1433;Initial Catalog=EveIntelManager20190521111543_db;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {}                
       public DbSet<Reports> Report { get; set; }
    }
}
