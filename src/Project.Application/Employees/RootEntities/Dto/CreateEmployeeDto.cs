using System;
using Abp.Application.Services.Dto;

namespace Project.Employees.RootEntities.Dto
{
   public class CreateEmployeeDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
    }
}

