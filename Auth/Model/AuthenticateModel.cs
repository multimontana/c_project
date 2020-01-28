using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Auth.Model
{
    /// <summary>
    /// Token Request
    /// </summary>
    public class AuthenticateModel
    {
        [Required] [JsonProperty("username")] public string Login { get; set; }

        [JsonProperty("host")] public string HostName { get; set; }

        [Required] [JsonProperty("password")] public string Password { get; set; }
    }
}