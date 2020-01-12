using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class PlayersVM
    {
        public List<AgeGroup> AgeGroups { get; set; }
        public Child Child { get; set; }
    }
}