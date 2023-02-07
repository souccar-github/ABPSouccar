using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Employees.Indecies.Dto;
using Project.Employees.Entities.Dto;
using Project.Employees.Enums;

namespace Project.Employees.RootEntities.Dto
{
   public class CreateEmployeeDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CreateNationalityDto Nationality { get; set; }
        public List<CreateCountryDto> Countries { get; set; }
        public List<CreateChildrenDto> Children { get; set; }
        public Gender Gender { get; set; }
    }
}

