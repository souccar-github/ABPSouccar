using System;
using Abp.Application.Services.Dto;

namespace Project.Employees.Entities.Dto
{
   public class CreateChildrenDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

