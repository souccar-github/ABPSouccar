import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListPageHeaderComponent } from './list-page-header/list-page-header.component';
import { HeadingComponent } from './heading/heading.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { ApplicationMenuComponent } from './application-menu/application-menu.component';


@NgModule({
  declarations: [
    ListPageHeaderComponent,
    HeadingComponent,
    ApplicationMenuComponent,
    BreadcrumbComponent,
  ],
  imports: [
    CommonModule
  ]
})
export class ContentTemplateModule { }
