using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Skeleton.DTO
{
    public class LoginDto
    {
        /// <summary>
        /// 
        /// </summary>
        [SwaggerSchema("User e-mail address")]
        [Required(ErrorMessage = "Email field is required")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SwaggerSchema("User password")]
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
