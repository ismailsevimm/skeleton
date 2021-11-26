using System;
using System.Collections.Generic;

namespace Skeleton.Services.Configuration
{
    public class ClientOption
    {
        public string Id { get; set; }

        public string Secret { get; set; }

        public List<String> Audiences { get; set; }
    }
}
