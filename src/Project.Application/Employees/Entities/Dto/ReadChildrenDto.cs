using System;
using Abp.Application.Services.Dto;

namespace Project.Employees.Entities.Dto
{
   public class ReadChildrenDto : EntityDto<int>
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}

