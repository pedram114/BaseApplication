using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Users
    {
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
        {

        public DateTime BirthDay { set; get; }
        public string WorkDescription { set; get; }

        // سایر خواص اضافی در اینجا

        [ForeignKey( "AddressId" )]
        public virtual Address Address { get; set; }
        public int? AddressId { get; set; }
        }
    }
