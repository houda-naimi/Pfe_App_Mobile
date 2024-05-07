using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using YesS;

namespace PfeShell.Models
{
    public class immeuble
    {
        public string TrancheName { get; set; }
        public Guid? TrancheGuid { get; set; }
        public ObservableCollection<Categories> categories { get; set; }
    }
}
