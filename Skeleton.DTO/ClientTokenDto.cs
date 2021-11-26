using System;

namespace Skeleton.DTO
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpiration { get; set; }
    }
}
