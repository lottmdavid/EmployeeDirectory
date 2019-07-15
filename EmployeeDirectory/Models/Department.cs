using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Display(Name = "Last Updated")]
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }

        public List<Employee> Employees { get; set; }
    }
}