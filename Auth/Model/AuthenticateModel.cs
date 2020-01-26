using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auth.Model
{
    /// <summary>
    /// Token Request
    /// </summary>
    public class AuthenticateModel
    {
        [Required] [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("host")] public string HostName { get; set; }

        [Required] [JsonProperty("password")] public string Password { get; set; }
    }
}