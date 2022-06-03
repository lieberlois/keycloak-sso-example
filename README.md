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

The applications both are secured, but in different approaches. 

### ProductApi
The `ProductApi` uses a classic RBAC-mechanism, where users that want to read the resource 
`Product` only have to have the role `read-product` linked to their account. 


### location-api
Having global roles in an IAM-solution does not only scale terribly, it also
prevents fine granular distribution of permissions. Because of this, the `location-api`
is protected on a *Resource* and *Scope* level. This includes the following parts:

* Resource: The resource `Location` that should be protected (/api/location/*)
* Authorization Scopes: Subsets of the resource, e.g. view, create, delete
* Policies: Rules like 'Only members of group xxx'
* Permissions: Mapping of Policies to Resources or Scopes within Resources

The current configuration allows members of the group `Viewer` to view locations,
while members of the group `Moderator` are able to create and delete locations.

To read more on authorization in Keycloak, check the [documentation](https://wjw465150.gitbooks.io/keycloak-documentation/content/authorization_services/index.html).