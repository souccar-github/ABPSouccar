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
            context.CreatePermission(PermissionNames.Pages_Employees, L("Employee"));
            context.CreatePermission(PermissionNames.Actions_Employees_Create, L("CreateNewEmployee"));
            context.CreatePermission(PermissionNames.Actions_Employees_Update, L("EditEmployee"));
            context.CreatePermission(PermissionNames.Actions_Employees_Delete, L("DeleteEmployee"));

            //Countries
            context.CreatePermission(PermissionNames.Pages_Countries, L("Country"));
            context.CreatePermission(PermissionNames.Actions_Countries_Create, L("CreateNewCountry"));
            context.CreatePermission(PermissionNames.Actions_Countries_Update, L("EditCountry"));
            context.CreatePermission(PermissionNames.Actions_Countries_Delete, L("DeleteCountry"));

            //Nationalities
            context.CreatePermission(PermissionNames.Pages_Nationalities, L("Nationality"));
            context.CreatePermission(PermissionNames.Actions_Nationalities_Create, L("CreateNewNationality"));
            context.CreatePermission(PermissionNames.Actions_Nationalities_Update, L("EditNationality"));
            context.CreatePermission(PermissionNames.Actions_Nationalities_Delete, L("DeleteNationality"));

            //Children
            context.CreatePermission(PermissionNames.Pages_Children, L("Children"));
            context.CreatePermission(PermissionNames.Actions_Children_Create, L("CreateNewChildren"));
            context.CreatePermission(PermissionNames.Actions_Children_Update, L("EditChildren"));
            context.CreatePermission(PermissionNames.Actions_Children_Delete, L("DeleteChildren"));




        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectConsts.LocalizationSourceName);
        }
    }
}
