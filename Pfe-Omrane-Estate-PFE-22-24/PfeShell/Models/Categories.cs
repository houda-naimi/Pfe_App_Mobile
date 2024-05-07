using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class Categories
    {
        public string NameCategorie { get; set; }
        public int ItemCount { get; set; }
        public Guid? CategorieGuid { get; set; }
        public Guid? TrancheGuid { get; set; }
    }
}
