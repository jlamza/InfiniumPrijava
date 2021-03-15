using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.SignalR
{
    public class UpdatesHub : Hub
    {

        
        public async Task SendMessageToCaller()
        {
            await Clients.Caller.SendAsync("RecieveMessage");
        }
    }
}
