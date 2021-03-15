using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Entities
{
    public class PhoneNumber
    {
        
        public int Id { get; set; }
        public string PNumber { get; set; }


        [Required]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        
    }
}
