using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Project.Authorization
{
    public class ProjectAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            // Auto generate dont remove this line
            //Employees
            context.CreatePermission(PermissionNames.Personnel_RootEntities_Read_Employees, L("Employee"));
            context.CreatePermission(PermissionNames.Personnel_RootEntities_Insert_Employees, L("CreateNewEmployee"));
            context.CreatePermission(PermissionNames.Personnel_RootEntities_Edit_Employees, L("EditEmployee"));
            context.CreatePermission(PermissionNames.Personnel_RootEntities_Delete_Employees, L("DeleteEmployee"));

            //Nationalities
            context.CreatePermission(PermissionNames.Personnel_Indecies_Read_Nationalities, L("Nationality"));
            context.CreatePermission(PermissionNames.Personnel_Indecies_Insert_Nationalities, L("CreateNewNationality"));
            context.CreatePermission(PermissionNames.Personnel_Indecies_Edit_Nationalities, L("EditNationality"));
            context.CreatePermission(PermissionNames.Personnel_Indecies_Delete_Nationalities, L("DeleteNationality"));

            //Children
            context.CreatePermission(PermissionNames.Personnel_Entities_Read_Children, L("Children"));
            context.CreatePermission(PermissionNames.Personnel_Entities_Insert_Children, L("CreateNewChildren"));
            context.CreatePermission(PermissionNames.Personnel_Entities_Edit_Children, L("EditChildren"));
            context.CreatePermission(PermissionNames.Personnel_Entities_Delete_Children, L("DeleteChildren"));


        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectConsts.LocalizationSourceName);
        }
    }
}
