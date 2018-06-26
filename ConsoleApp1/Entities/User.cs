using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.Entities
{
    public class User 
    {
        [Key]
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
