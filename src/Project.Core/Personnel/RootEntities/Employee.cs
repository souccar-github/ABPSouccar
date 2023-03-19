using Project.Personnel.Entities;
using Project.Personnel.Enums;
using Project.Personnel.Indecies;
using Project.Souccar.Attributes;
using Project.Souccar.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Personnel.RootEntities
{
    public class Employee : SouccarAggregate
    {
        public Employee()
        {
            Children = new List<Children>();
        }
        [SouccarUIP(ForDropDown = true)]
        public string NameForDropDown { get { return this.FirstName + " " + this.LastName; } }
        [SouccarUIP(ForGridView = true)]
        public string FirstName { get; set; }
        [SouccarUIP(ForGridView = true)]
        public string LastName { get; set; }
        [ForeignKey("Nationality")]
        public int NationalityId { get; set; }
        public Nationality Nationality { get; set; }
        public Gender Gender { get; set; }
        public List<Children> Children { get; set; }
    }
}
