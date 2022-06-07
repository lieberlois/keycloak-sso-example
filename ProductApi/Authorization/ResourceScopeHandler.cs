using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json;

namespace ProductApi.Authorization
{
    public class ResourceScopeHandler : AuthorizationHandler<ResourceScopeRequirement>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ResourceScopeHandler> _logger;


        public ResourceScopeHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ResourceScopeHandler> logger
        )
        {
            this._httpClient = httpClient;
            this._httpContextAccessor = httpContextAccessor;
            this._logger = logger;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceScopeRequirement resourceScopeRequirement
        )
        {
            // Build permission, e.g. Product#view
            var permission = resourceScopeRequirement.Resource;
            if (resourceScopeRequirement.Scope != null)
            {
                permission += $"#{resourceScopeRequirement.Scope}";
            }

            var success = await PerformRequest(permission);

            if (success)
            {
                this._logger.LogInformation($"Granting permission on {permission}");
                context.Succeed(resourceScopeRequirement);
            }
            else
            {
                this._logger.LogInformation($"Forbidding permission on {permission}");
                context.Fail();
            }
        }

        private async Task<bool> PerformRequest(string permission)
        {
            try
            {
                // Note: This URL should be extracted into the project configuration since it is duplicated several times
                var endpoint = "http://localhost:8080/auth/realms/SSO/protocol/openid-connect/token";
                var token = await this._httpContextAccessor.HttpContext.GetTokenAsync("access_token");

                var data = new[]
                {
                    new KeyValuePair<string, string>("audience", "product-api"),
                    new KeyValuePair<string, string>("response_mode", "decision"),
                    new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"),
                    new KeyValuePair<string, string>("permission", permission),
                    new KeyValuePair<string, string>("content-type", "application/x-www-form-urlencoded"),
                };

                this._httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var response = await this._httpClient.PostAsync(endpoint, new FormUrlEncodedContent(data));

                if (response.IsSuccessStatusCode)
                {
                    // The API responds whether access shall be granted
                    var success = await response.Content.ReadAsStringAsync();
                    return success == "{\"result\":true}";
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}