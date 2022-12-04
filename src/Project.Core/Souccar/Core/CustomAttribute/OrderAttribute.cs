using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Souccar.Core.CustomAttribute
{
    /// <summary>
    /// Author: Yaseen Alrefaee
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class OrderAttribute:Attribute
    {
        public OrderAttribute(int order)
        {
            this.Order = order;
        }
        public int Order { get; set; }
    }
}
