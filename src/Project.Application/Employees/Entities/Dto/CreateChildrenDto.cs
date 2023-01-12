using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Employees.RootEntities.Dto;

namespace Project.Employees.Entities.Dto
{
   public class CreateChildrenDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CreateEmployeeDto Employee { get; set; }
    }
}

