using Project.Personnel.RootEntities;
using Project.Souccar.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Personnel.Entities
{
    public class Children : SouccarEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
