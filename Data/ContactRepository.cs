using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projekt.DTOs;
using Projekt.Entities;
using Projekt.Helpers;
using Projekt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }





        public async void AddContact(AppUser user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> CheckIfConstrained(AppUser user)
        {
            return await _context.Users.AnyAsync(x => x.Name == user.Name && x.Address == user.Address);
        }

        public async Task<bool> CheckIfExists(string SomeExpresssion)
        {
            return await _context.Users.AnyAsync(p => p.Name == SomeExpresssion);
        }

        public bool CheckIfNameChanged(AppUser user_1, AppUser user_2)
        {
            if ((user_1.Name != user_2.Name) || (user_1.Address != user_2.Address)) return true;
            else return false;
        }

        public void DeleteContact(AppUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().Include(p => p.PhoneNumbers).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams)
        {
            var query = _context.Users.Include(p => p.PhoneNumbers)
                .AsNoTracking().OrderBy(p=>p.Id);
            return await PagedList<AppUser>.CreateAsync(query, userParams.pageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {

            _context.Users.Update(user);
            
        }
    }
}
