using AutoMapper;
using Projekt.DTOs;
using Projekt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<AppUser, AppUser>();
        }
    }
}
