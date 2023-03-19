using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Project.Souccar.Application.Dtos;
using Project.Personnel.Indecies.Dto;
using Project.Personnel.Entities.Dto;
using Project.Personnel.Enums;

namespace Project.Personnel.RootEntities.Dto
{
   public class UpdateEmployeeDto : EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NationalityId { get; set; }
        public UpdateNationalityDto Nationality { get; set; }
        public List<UpdateChildrenDto> Children { get; set; }
        public Gender Gender { get; set; }
    }
}

