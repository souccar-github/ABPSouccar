using System;
using System.Collections.Generic;
using Project.Authorization.Users;
using Project.Souccar.Core.CustomAttribute;
using Project.Souccar.Domain.DomainModel;
using Project.Souccar.Domain.Workflow.Entities;
using Project.Souccar.Domain.Workflow.Enums;

namespace Project.Souccar.Domain.Workflow.RootEntities
{
    /// <summary>
    /// Author: Yaseen Alrefaee
    /// </summary>

    [Module("Workflow")]
    public class WorkflowItem : SouccarAggregate
    {

        public WorkflowItem()
        {
            Date = DateTime.Now;
            Steps=new List<WorkflowStep>();
            Approvals = new List<WorkflowApproval>();
        }

        #region basic info

        public virtual DateTime Date { get; set; }
        public virtual string Description { get; set; }
        public virtual WorkflowStatus Status { get; set; }
        public virtual WorkflowType Type { get; set; }
        [UserInterfaceParameter(IsReference = true)]
        public virtual User Creator { get; set; }
        [UserInterfaceParameter(IsReference = true)]
        public virtual User FirstUser { get; set; }
        [UserInterfaceParameter(IsReference = true)]
        public virtual User CurrentUser { get; set; }

        [UserInterfaceParameter(IsReference = true)]
        public virtual User TargetUser { get; set; }
        public virtual int StepCount { get; set; }
        [UserInterfaceParameter(IsHidden = true)]
        public virtual string NameForDropdown { get { return  TargetUser == null ? Date.ToString("d") : Date.ToString("d") + " " + TargetUser.NameForDropdown; } }
        #endregion

        public virtual IList<WorkflowStep> Steps { get; set; }
        public virtual void AddStep(WorkflowStep step)
        {
            step.Workflow = this;
            this.Steps.Add(step);
        }

        public virtual IList<WorkflowApproval> Approvals { get; set; }
        public virtual void AddApproval(WorkflowApproval approval)
        {
            approval.Workflow = this;
            this.Approvals.Add(approval);
        }
    }
}


