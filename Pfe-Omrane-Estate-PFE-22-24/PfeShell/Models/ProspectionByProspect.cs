using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class ProspectionByProspect
    {
        public string TrancheName { get; set; }
        public DateTimeOffset? DateProspection { get; set; }
        public string CategorieName { get; set; }
        public string TypeName { get; set; }
        public Guid ProspectionId { get; set; }
        public Guid? TrancheId { get; set; }
        
    }
}
