using Projekt.DTOs;
using Projekt.Entities;
using Projekt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Interfaces
{
    public interface IContactRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string name);
        Task<bool> CheckIfExists(string name);
        Task<bool> CheckIfConstrained(AppUser user);
        bool CheckIfNameChanged(AppUser user_1, AppUser user_2);
        void AddContact(AppUser user);

        void DeleteContact(AppUser user);


    }
}
