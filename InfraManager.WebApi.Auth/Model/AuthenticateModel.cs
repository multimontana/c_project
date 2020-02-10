namespace InfraManager.WebApi.Auth.Model
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class AuthenticateModel
    {
        [Required]
        [JsonProperty("username")]
        public string Login { get; set; }

        [JsonProperty("host")]
        public string HostName { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}