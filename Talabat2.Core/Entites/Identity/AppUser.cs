using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites.Identity
{
    public class AppUser:IdentityUser
    {
        //Create AppUser Class To Add Some Attributes TO IdentityUser
        public string DisplayName { get; set; }
       
        public Address Address { get; set; }//Navaiginational Property => One
    }
}
