using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Souccar.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SouccarUIPAttribute : Attribute
    {
        public bool ForGridView { get; set; }
        public bool ForDropDown { get; set; }
    }
}
