using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PfeShell.Models
{
    public class AdmLicense
    {
        [JsonProperty("description")]
        string Description { get; set; }

        [JsonProperty("admUserEmail")]
        string AdmUserEmail { get; set; }
    }
}
