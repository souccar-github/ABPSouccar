using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Employees.Indecies.Dto;
using Project.Employees.Entities.Dto;
using Project.Employees.Enums;

namespace Project.Employees.RootEntities.Dto
{
   public class EmployeeDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public NationalityDto Nationality { get; set; }
        public List<CountryDto> Countries { get; set; }
        public List<ChildrenDto> Children { get; set; }
        public Gender Gender { get; set; }
    }
}

