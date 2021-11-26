using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Skeleton.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class RefreshTokenDto
    {
        /// <summary>
        /// 
        /// </summary>
        [SwaggerSchema("RefreshToken is created when user logged in and used for quick login ")]
        [Required(ErrorMessage = "RefreshToken field is required")]
        public string RefreshToken { get; set; }
    }
}
