using Microsoft.AspNetCore.Authorization;
using ZANECO.API.Shared.Authorization;

namespace ZANECO.API.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = FSHPermission.NameFor(action, resource);
}