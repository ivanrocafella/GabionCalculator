using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Gabion> Gabions { get; set; }

        public User()
        {
            Gabions = new HashSet<Gabion>();
        }
    }
}
