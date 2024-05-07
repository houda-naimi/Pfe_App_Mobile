using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class CategorieRec
    {

        public int idCat { get; set; }
        public string code { get; set; }
        public string descriptionCat { get; set; }
        public List<object> reclamations { get; set; }
    }
}
