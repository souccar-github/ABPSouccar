using System;
using Abp.Application.Services.Dto;

namespace Project.Employees.Entities.Dto
{
   public class ChildrenDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

