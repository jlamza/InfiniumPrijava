using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.DTOs;
using Projekt.Entities;
using Projekt.Helpers;
using Projekt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactsController(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }






        #region API CALLS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            //Ego loading phone numbers, gives circular reference problem,
            //use DTOs and Mapping instead

            var users = await _contactRepository.GetUsersAsync(userParams);
            Response.AddpaginationHeader(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);
            var usersToReturn = _mapper.Map<IEnumerable<AppUserDto>>(users);
            
            return Ok(usersToReturn);
            
        }




        //~/api/Contacts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _contactRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<AppUserDto>(user);
            return Ok(userToReturn);
           
        }





        [HttpPost("addContact")]
        public async Task<ActionResult> AddContact(AppUser appUser)
        {


            var IsConstrained = await _contactRepository.CheckIfConstrained(appUser);
            if (IsConstrained)
            {
                return BadRequest("user already exists");
            }
            else
            {
                _contactRepository.AddContact(appUser);
                var result = await _contactRepository.SaveAllAsync();

                if (result)
                    return Ok();
                else
                    return BadRequest("user not saved to database");
            }
        }




        [HttpPut("update")]
        public async Task<ActionResult> Update(AppUser user)
        {

            //Check if user changed his name, if changed ->check if it's unique then update,
            //if name not changed just update
            var UserBeforeUpdate = _contactRepository.GetUserByIdAsync(user.Id);
            
            var IsNameChangedBool = _contactRepository.CheckIfNameChanged(user , UserBeforeUpdate.Result);
           
            if (IsNameChangedBool)
            {
                var IsConstrained = await _contactRepository.CheckIfConstrained(user);
                if (IsConstrained)
                {
                    return BadRequest("user already exists");
                }
                else
                {
                    _contactRepository.Update(user);
                    if (await _contactRepository.SaveAllAsync()) return Ok("Contact updated");
                    return BadRequest("user not saved to database");
                }
            }
            else
            {
                _contactRepository.Update(user);
                if (await _contactRepository.SaveAllAsync()) return Ok("Contact updated");
                return BadRequest("user not saved to database");
            } 
        }


       [HttpDelete("delete/{id}")]
        public async Task<ActionResult> remove(int id)
        {
            var userToDelete = await _contactRepository.GetUserByIdAsync(id);
            _contactRepository.DeleteContact(userToDelete);
            if (await _contactRepository.SaveAllAsync()) return Ok("Contact deleted");
            return BadRequest("Error deleting contact");
        }




        #endregion

    }
}
