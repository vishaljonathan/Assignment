using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagementApplication.Entity
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Assets> AssetsOwned { get; set; } = new List<Assets>();
        public Employees(int employeeId, string name, string department, string email, string password)
        {
            EmployeeID = employeeId;
            Name = name;
            Department = department;
            Email = email;
            Password = password;
        }
    }
}
