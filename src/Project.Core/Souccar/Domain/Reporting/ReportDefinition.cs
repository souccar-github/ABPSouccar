using Project.Authorization.Users;
using Project.Souccar.Core.CustomAttribute;
using Project.Souccar.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Souccar.Domain.Reporting
{
    [Module("Reporting")]
    public class ReportDefinition:SouccarAggregate
    {
        public ReportDefinition() 
        {
            CreationDate = DateTime.Now;
        }
        [UserInterfaceParameter(Order = 1)]
        public virtual string Title { get; set; }

        [UserInterfaceParameter(Order = 2)]
        public virtual string Description { get; set; }

        [UserInterfaceParameter(Order = 3,IsNonEditable=true)]
        public virtual DateTime CreationDate { get; set; }

        [UserInterfaceParameter(Order = 4, IsNonEditable=true)]
        public virtual User CreatedBy { get; set; }

        [UserInterfaceParameter(Order = 5)]
        public virtual string ModuleName { get; set; }

        [UserInterfaceParameter(IsFile = true,AcceptExtension=".rdl",FileSize=1000000)]
        public virtual string FileName { get; set; }

    }
}


