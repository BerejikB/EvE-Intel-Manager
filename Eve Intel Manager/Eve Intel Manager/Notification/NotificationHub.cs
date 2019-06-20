using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNetCore.SignalR;

namespace Eve_Intel_Manager.Notification
{
    public class Notifications : Hub
    {
        public string CharName { get; set; }

        public string ReportGenerated { get; set; }

        public string ReportSystem { get; set; }

        public string ReportBody { get; set; }
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("receiveMessage", CharName, ReportGenerated, ReportSystem,  ReportBody);
        }
    }

        

   
}