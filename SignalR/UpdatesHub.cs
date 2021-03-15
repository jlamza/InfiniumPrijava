using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Projekt.DTOs;
using Projekt.Helpers;
using Projekt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt.SignalR
{
    public class UpdatesHub : Hub
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public UpdatesHub(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        
        
        public async Task SendMessageToCaller(UserParams userParams)
        {
            
            var users = await _contactRepository.GetUsersAsync(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<AppUserDto>>(users);

            await Clients.Caller.SendAsync("RecieveMessage", usersToReturn);
        }
    }
}
