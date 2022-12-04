using System;
using Abp.Application.Services.Dto;

namespace Project.Employees.Entities.Dto
{
   public class UpdateChildrenDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

