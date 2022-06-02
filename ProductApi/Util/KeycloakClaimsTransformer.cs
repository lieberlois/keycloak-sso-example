using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductApi.Util
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is null)
            {
                return Task.FromResult(principal);
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;

            if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == "resource_access"))
            {
                var realmAccessClaim = claimsIdentity.FindFirst((claim) => claim.Type == "resource_access");

                if (realmAccessClaim is null)
                {
                    return Task.FromResult(principal);
                }

                var realmAccessAsDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string[]>>>(realmAccessClaim.Value);

                // Note: 'product-api' should be extracted into the project configuration since it is duplicated several times
                if (realmAccessAsDict is not null && realmAccessAsDict.ContainsKey("product-api") && realmAccessAsDict["product-api"].ContainsKey("roles"))
                {
                    foreach (var role in realmAccessAsDict["product-api"]["roles"])
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            return Task.FromResult(principal);
        }
    }
}