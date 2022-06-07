# keycloak-sso-example

The purpose of this repository is the demonstration of a small SSO setup using Keycloak.

## Services

| Service               | Address                | Technology             |
|-----------------------|------------------------|------------------------|
| Client                | http://localhost:4200  | Angular                |
| Keycloak              | http://localhost:8080  | Keycloak               |
| location-api          | http://localhost:3000  | NestJS                 |
| ProductApi            | http://localhost:5000  | ASP.NET Core           |

## Users and Group Memberships  

| Service               | Username               | Password               | Group Membership       |
|-----------------------|------------------------|------------------------|------------------------|
| Angular Client        | demo                   | demo                   | Viewer                 |
| Angular Client        | demo2                  | demo2                  | Moderator              |
| Angular Client        | demo3                  | demo3                  | -                      |
| Keycloak              | admin                  | admin                  | Keycloak Admin         |


## Authentication & Authorization

Having global roles in an IAM-solution does not only scale terribly, it also
prevents fine granular distribution of permissions. Because of this, the APIs are not only using authentication and role-based authorization, but also a Resource Server style approach.

This includes the following parts:

* Resource: The resource `e.g.Location` that should be protected (e.g. /api/location/*)
* Authorization Scopes: Subsets of the possible permissions to the resource, e.g. view, create, delete
* Policies: Rules like 'Only members of group xxx'
* Permissions: Mapping of Policies to Resources or Scopes within Resources

### ProductApi
The `ProductApi` uses multiple authorization approaches. The Route `/api/product` is protected with a classic role-based authorization where users that want to read the resource `Product` only have to have the role `read-product` linked to their account. This was achieved by implementing a custom `IClaimsTransformation` that maps the roles from the JWT coming from Keycloak to Microsoft Identity Roles. Users `demo` and `demo2` can view all products, but only `demo2` can view the example product.

The route `/api/product/example` on the other hand is protected like a resource in a `Resource Server`. The permission `Example - Product Permission` only allows members of the group `Viewer` to access the route. This was achieved by adding a custom `IAuthorizationRequirement` with its respective `AuthorizationHandler<ResourceScopeRequirement>`. When a resource that is protected by such a policy is requested, a POST-request to Keycloak enforces the permissions and verifies, that the current user (based on the JWT within the request) has access to the requested resource.

### location-api
The current configuration allows members of the group `Viewer` to view locations, while members of the group `Moderator` are able to create and delete locations. The implementation does approximately the same as the one in the `ProductApi`, however the library `nest-keycloak-connect` provided most of the functionality out of the box. Users `demo` and `demo2` can view all locations, but only `demo2` can create and delete locations.

To read more on authorization in Keycloak, check the [documentation](https://wjw465150.gitbooks.io/keycloak-documentation/content/authorization_services/index.html).