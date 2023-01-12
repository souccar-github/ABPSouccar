using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Employees.Indecies.Dto;
using Project.Employees.Entities.Dto;
using Project.Employees.Enums;

namespace Project.Employees.RootEntities.Dto
{
   public class UpdateEmployeeDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UpdateNationalityDto Nationality { get; set; }
        public List<UpdateCountryDto> Countries { get; set; }
        public List<UpdateChildrenDto> Children { get; set; }
        public Gender Gender { get; set; }
    }
}

