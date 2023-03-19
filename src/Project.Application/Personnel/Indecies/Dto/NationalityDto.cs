using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Souccar.Application.Dtos;

namespace Project.Personnel.Indecies.Dto
{
   public class NationalityDto : EntityDto<int>
    {
        public string Name {get; set;}
        public int Order {get; set;}
    }
}

