using Microsoft.AspNetCore.Authorization;

namespace ProductApi.Authorization
{
    public class ResourceScopeRequirement : IAuthorizationRequirement
    {
        public ResourceScopeRequirement(string resource, string? scope)
        {
            this.Resource = resource;
            this.Scope = scope;
        }

        public string Resource { get; set; }
        public string? Scope { get; set; }
    }
}