using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.DTOs
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime dateOfBirth { get; set; }

        public string Address { get; set; }
        public ICollection<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}
