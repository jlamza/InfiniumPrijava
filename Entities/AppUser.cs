using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime dateOfBirth { get; set; }

        public string Address { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers{ get; set; }
    }
}
