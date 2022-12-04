using System;
using Abp.Application.Services.Dto;

namespace Project.Employees.RootEntities.Dto
{
   public class ReadEmployeeDto : EntityDto<int>
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int gender { get; set; }
    }
}

