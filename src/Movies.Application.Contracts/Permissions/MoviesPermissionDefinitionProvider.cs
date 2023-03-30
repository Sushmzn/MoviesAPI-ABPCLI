using Movies.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Movies.Permissions;

public class MoviesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MoviesPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MoviesPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MoviesResource>(name);
    }
}
