using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Project.Souccar.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Souccar.Domain.DomainModel
{
    public class SouccarIndex : FullAuditedAggregateRoot, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        [SouccarUIP(ForDropDown =true)]
        public virtual string Name { get; set; }
        public virtual int Order { get; set; }
    }
}
