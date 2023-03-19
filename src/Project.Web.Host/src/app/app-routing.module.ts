import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { TwoListDragAndDropComponent } from './widgets/two-lists-drag-and-drop/two_list_drag_and_drop.component';
import { EmployeeComponent } from './personnel/root-entities/employee/employee.component';
import { ChildrenComponent } from './personnel/entities/children/children.component';
import { NationalityComponent } from './personnel/indecies/nationality/nationality.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] },
                    { path: 'two-lists', component: TwoListDragAndDropComponent },
                    {
                        path: 'personnel/employees',
                        component: EmployeeComponent,
                        children: [
                            { path: 'children', component: ChildrenComponent, data: { permission: 'Personnel.Entities.Read.Children' }, canActivate: [AppRouteGuard] }
                        ],
                        data: { permission: 'Personnel.RootEntities.Read.Employees' }, canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'personnel/nationalities', component: NationalityComponent, data: { permission: 'Personnel.Indecies.Read.Nationalities' }, canActivate: [AppRouteGuard]
                    }
                ]
            },

        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
