using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EmployeeManagement.Models
{
    public class DepartmentModel
    {
        [Key]
        [Display(Name = "Department ID")]
        [Required(ErrorMessage = "Id is mandatory")]
        public int DepartmentID { get; set; }
        [Display(Name = "Department Name")]
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string DepartmentName { get; set; } 
        public string Description { get; set; }
    }
}