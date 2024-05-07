using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class DossierReclamation
    {
        public string coddossier { get; set; }
        public DateTime datedossier { get; set; }
        public string vocation { get; set; }
        public string entite { get; set; }
        public string tranche { get; set; }
        public string client { get; set; }
        public bool garantie { get; set; }
        public string origine { get; set; }

    }
}
