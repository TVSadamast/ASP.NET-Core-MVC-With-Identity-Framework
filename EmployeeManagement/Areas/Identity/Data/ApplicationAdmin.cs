using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationAdmin class
    public class ApplicationAdmin : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        public string Surname { get; set; }
    }
}
