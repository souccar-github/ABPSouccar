using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Project.Employees.Entities;
using Project.Employees.Entities.Dto;
using Project.Employees.Indecies;
using Project.Employees.Indecies.Dto;

namespace Project.Employees.RootEntities.Dto
{
   public class EmployeeDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public NationalityDto Nationality { get; set; }
        public List<CountryDto> Countries { get; set; }
        public List<ChildrenDto> Children { get; set; }
    }
}

