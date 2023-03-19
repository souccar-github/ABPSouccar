using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Souccar.Application.Dtos;
using Project.Personnel.RootEntities.Dto;

namespace Project.Personnel.Entities.Dto
{
   public class UpdateChildrenDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeId { get; set; }
        public UpdateEmployeeDto Employee { get; set; }
    }
}

