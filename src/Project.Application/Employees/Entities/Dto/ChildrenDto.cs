using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Employees.RootEntities.Dto;

namespace Project.Employees.Entities.Dto
{
   public class ChildrenDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}

