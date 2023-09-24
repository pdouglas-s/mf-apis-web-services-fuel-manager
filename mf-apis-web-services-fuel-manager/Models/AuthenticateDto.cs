using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace mf_apis_web_services_fuel_manager.Models
{
    public class AuthenticateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
