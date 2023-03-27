import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeComponent } from './root-entities/employee/employee.component';
import { CreateEmployeeDialogComponent } from './root-entities/employee/create-employee/create-employee-dialog.component';
import { EditEmployeeDialogComponent } from './root-entities/employee/edit-employee/edit-employee-dialog.component';
import { NationalityComponent } from './indecies/nationality/nationality.component';
import { ChildrenComponent } from './entities/children/children.component';
import { ContentTemplateModule } from '@app/layout/content-template/content-template.module';
import { SharedModule } from '@shared/shared.module';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { TranslateModule } from '@ngx-translate/core';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';



@NgModule({
  declarations: [
    EmployeeComponent,
    CreateEmployeeDialogComponent,
    EditEmployeeDialogComponent,
    NationalityComponent,
    ChildrenComponent
  ],
  imports: [
    CommonModule,
    ContentTemplateModule,
    SharedModule,
    PerfectScrollbarModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    TranslateModule,
    CollapseModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
  ],
  exports: [
    EmployeeComponent,
    NationalityComponent,
    ChildrenComponent,
    CreateEmployeeDialogComponent,
    EditEmployeeDialogComponent,
  ],
  entryComponents:[
    CreateEmployeeDialogComponent,
    EditEmployeeDialogComponent,
  ],
  schemas: [
    NO_ERRORS_SCHEMA
  ],
})
export class PersonnelModule { }
