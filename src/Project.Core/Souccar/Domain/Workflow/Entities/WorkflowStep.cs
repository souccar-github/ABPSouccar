using System;
using Project.Authorization.Users;
using Project.Souccar.Core.CustomAttribute;
using Project.Souccar.Domain.DomainModel;
using Project.Souccar.Domain.Workflow.Enums;
using Project.Souccar.Domain.Workflow.RootEntities;

namespace Project.Souccar.Domain.Workflow.Entities
{
    /// <summary>
    /// Author: Yaseen Alrefaee
    /// </summary>
    public class WorkflowStep : SouccarEntity 
    {
        #region basic info
        public virtual DateTime Date { get; set; }
        public virtual int Order { get; set; }
        public virtual string Description { get; set; }
        public virtual WorkflowStepStatus Status { get; set; }

        [UserInterfaceParameter(IsReference = true)]
        public virtual User User { get; set; }
        #endregion

        public virtual WorkflowItem Workflow { get; set; }
    }
}
