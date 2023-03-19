using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Souccar.Application.Dtos
{
    public class SouccarPagedResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
    }
}
