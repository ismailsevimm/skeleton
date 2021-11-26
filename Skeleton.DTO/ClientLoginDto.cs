using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Skeleton.DTO
{
    public class ClientLoginDto
    {
        /// <summary>
        /// 
        /// </summary>
        [SwaggerSchema("ClientId")]
        [Required(ErrorMessage = "ClientId field is required")]
        public string ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SwaggerSchema("ClientSecret")]
        [Required(ErrorMessage = "ClientSecret field is required")]
        public string ClientSecret { get; set; }
    }
}
