using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.ViewModels
{
    public class AcceptApplicationVM
    {
        public IList<string> SelectedPlayers { get; set; }
        public IList<SelectListItem> PlayersToRegister { get; set; }
        public AcceptApplicationVM()
        {
            SelectedPlayers = new List<string>();
            PlayersToRegister = new List<SelectListItem>();
        }

    }
}