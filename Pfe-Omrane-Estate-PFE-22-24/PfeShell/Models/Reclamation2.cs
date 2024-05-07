using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class Reclamation2
    {
        public int idRec { get; set; }
        public int idclient { get; set; }
        public int idCat { get; set; }
        public DateTime dateRec { get; set; }
        public string description { get; set; }
        public string adresse { get; set; }
        public object idCatNavigation { get; set; }
        public object idclientNavigation { get; set; }
       // public CategorieRec idCatNavigation { get; set; }
       // public ClientRec idclientNavigation { get; set; }
        public string catRec { get; set; }
        public string nomClient { get; set; }
    }
}
