using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class ClientRec
    {
        public int idclient { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string numtel { get; set; }
        public string email { get; set; }
        public List<object> reclamations { get; set; }
    }
}
