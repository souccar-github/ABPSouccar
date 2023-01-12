using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Project.Employees.Indecies.Dto
{
   public class ReadCountryDto : EntityDto<int>
    {
        public string Name {get; set;}
        public int Order {get; set;}
    }
}

