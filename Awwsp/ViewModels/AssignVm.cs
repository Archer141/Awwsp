using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class AssignVm
    {
    public List<AgeGroup> AgeGroups { get; set; }
    public Trophy Trophy { get; set; }

    }

    public class AssignConfirmVM
    {
        public Trophy Trophy { get; set; }
        public AgeGroup AgeGroup { get; set; }
    }
}