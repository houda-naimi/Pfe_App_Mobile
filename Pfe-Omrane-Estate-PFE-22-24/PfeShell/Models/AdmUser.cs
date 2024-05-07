using Newtonsoft.Json;
using System;

namespace PfeShell.Models
{
    public class AdmUser
    {
        [JsonProperty("pKey")]
        Guid Pkey { get; set; }

        [JsonProperty("code")]
        string Code { get; set; }

        [JsonProperty("pass")]
        string pass { get; set; }

        [JsonProperty("description")]
        string Description { get; set; }

        [JsonProperty("userCreate")]
        string UserCreate { get; set; }

        [JsonProperty("userUpdate")]
        string UserUpdate { get; set; }

        [JsonProperty("emailPassword")]
        string EmailPassword { get; set; }

        [JsonProperty("emailSmtpserver")]
        string EmailSmtpserver { get; set; }

        [JsonProperty("openBeeCode")]
        string OpenBeeCode { get; set; }

        [JsonProperty("openBeePass")]
        string OpenBeePass { get; set; }
    }
}
