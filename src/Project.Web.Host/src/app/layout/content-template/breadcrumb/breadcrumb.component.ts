import { Component, Injector, Input } from '@angular/core';
import { Router, Event, NavigationEnd, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';
import { environment } from 'environments/environment';
import { filter, map } from 'rxjs/operators';
import { SidebarComponent } from '../../sidebar/sidebar.component';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html'
})

export class BreadcrumbComponent extends AppComponentBase {
  @Input() title = '';
  menuItems: MenuItem[];

  path = '';
  pathArr: string[] = [];

  constructor(injector:Injector,private router: Router, private activatedRoute: ActivatedRoute,private sidebarCom : SidebarComponent) {
    super(injector);
    this.menuItems = sidebarCom.getMenuItems();
    this.router.events
    .pipe(
      filter((event) => event instanceof NavigationEnd),
      map(() => this.activatedRoute),
      map((route) => {
        while (route.firstChild) { route = route.firstChild; }
        return route;
      })
    ).subscribe((event) => {
      this.path = this.router.url.slice(1, this.router.url.split('?')[0].length);
      const paramtersLen = Object.keys(event.snapshot.params).length;
      this.pathArr = this.path.split('/').slice(0, this.path.split('/').length - paramtersLen);
    });
  }

  getUrl = (sub: string) => {
    return '/' + this.path.split(sub)[0] + sub;
  }

  getLabel(path): string {
    if (path === environment.adminRoot) {
      return this.l("Home");
    }

    // step 0
    let foundedMenuItem = this.menuItems.find(x => x.route === path);

    if (!foundedMenuItem) {
      // step 1
      this.menuItems.map(menu => {
        if (!foundedMenuItem && menu.children) {foundedMenuItem = menu.children.find(x => x.route === path); }
      });
      if (!foundedMenuItem) {
        // step 2
        this.menuItems.map(menu => {
          if (menu.children) {
            menu.children.map(sub => {
                if (!foundedMenuItem && sub.children) {foundedMenuItem = sub.children.find(x => x.route === path); }
              });
          }
        });
        if (!foundedMenuItem) {
          // step 3
          this.menuItems.map(menu => {
            if (menu.children) {
              menu.children.map(sub => {
                if (sub.children) {
                  sub.children.map(deepSub => {
                    if (!foundedMenuItem && deepSub.children) {foundedMenuItem = deepSub.children.find(x => x.route === path); }
                  });
                }
              });
            }
          });
        }
      }
    }

    if (foundedMenuItem) { return this.l(foundedMenuItem.label); } else { return ''; }
  }

}
