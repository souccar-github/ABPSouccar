import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListPageHeaderComponent } from './list-page-header/list-page-header.component';
import { HeadingComponent } from './heading/heading.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { ApplicationMenuComponent } from './application-menu/application-menu.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    ListPageHeaderComponent,
    HeadingComponent,
    ApplicationMenuComponent,
    BreadcrumbComponent,
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[
    ListPageHeaderComponent,
    HeadingComponent,
    ApplicationMenuComponent,
    BreadcrumbComponent,
  ],
  schemas: [
    NO_ERRORS_SCHEMA
  ],
})
export class ContentTemplateModule { }
