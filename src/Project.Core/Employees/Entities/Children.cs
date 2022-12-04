using Project.Employees.RootEntities;
using Project.Souccar.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Employees.Entities
{
    public class Children : SouccarEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Employee Employee { get; set; }
    }
}
