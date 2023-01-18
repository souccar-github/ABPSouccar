import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { HeaderLeftNavbarComponent } from './header/header-left-navbar.component';
import { HeaderLogoComponent } from './header/header-logo.component';
import { HeaderUserMenuComponent } from './header/header-user-menu.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import {ContentTemplateModule} from './content-template/content-template.module';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';


@NgModule({
  declarations: [
    HeaderComponent,
    HeaderLeftNavbarComponent,
    HeaderLogoComponent,
    HeaderUserMenuComponent,
    FooterComponent,
    SidebarComponent,
  ],
  providers: [
    SidebarComponent,
  ],
  imports: [
    CommonModule,
    ContentTemplateModule,
    PerfectScrollbarModule,

  ],
  schemas: [
    NO_ERRORS_SCHEMA
  ],
})
export class LayoutModule { }
