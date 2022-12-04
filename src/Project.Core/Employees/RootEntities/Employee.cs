using Project.Employees.Entities;
using Project.Employees.Enums;
using Project.Employees.Indecies;
using Project.Souccar.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Employees.RootEntities
{
    public class Employee : SouccarAggregate
    {
        public Employee()
        {
            Children = new List<Children>();
            Countries = new List<Country>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Nationality Nationality { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<Children> Children { get; set; }
    }
}
