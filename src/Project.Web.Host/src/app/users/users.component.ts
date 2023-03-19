import { Component, Injector, ViewChild } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Hotkey, HotkeysService } from 'angular2-hotkeys';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  UserDto,
  UserDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateUserDialogComponent } from './create-user/create-user-dialog.component';
import { EditUserDialogComponent } from './edit-user/edit-user-dialog.component';
import { ResetPasswordDialogComponent } from './reset-password/reset-password.component';
import { ContextMenuComponent } from 'ngx-contextmenu';


class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  orderBy: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './users.component.html',
  animations: [appModuleAnimation()]
})
export class UsersComponent extends PagedListingComponentBase<UserDto> {
  users: UserDto[] = [];
  title = "Users"
  keyword = '';
  displayMode = 'list';
  itemOrder = { label: this.l("FullName"), value: "fullName" };
  itemOptionsOrders = [{ label: this.l("FullName"), value: "fullName" },
  { label: this.l("Username"), value: "userName" }];
  itemsPerPage = 10;
  selectAllState = '';
  selected: UserDto[] = [];
  data: UserDto[] = [];
  currentPage = 1;
  search = '';
  selectedCount = 0;
  isActive: boolean | null = true;
  advancedFiltersVisible = false;

  @ViewChild('basicMenu') public basicMenu: ContextMenuComponent;
  @ViewChild('addNewModalRef', { static: true }) addNewModalRef: CreateUserDialogComponent;

  constructor(
    private hotkeysService: HotkeysService,
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
    this.hotkeysService.add(new Hotkey('ctrl+a', (event: KeyboardEvent): boolean => {
      this.selected = [...this.data];
      return false;
    }));
    this.hotkeysService.add(new Hotkey('ctrl+d', (event: KeyboardEvent): boolean => {
      this.selected = [];
      return false;
    }));
  }

  ngOnInit(): void {
    this.loadData(this.itemsPerPage, 1, this.search, this.itemOrder.value);
  }

  changeOrderBy(item: any): void {
    this.loadData(this.itemsPerPage, 1, this.search, item.value);
  }

  changeDisplayMode(mode): void {
    this.displayMode = mode;
  }

  showAddNewModal(): void {
    let createOrEditUserDialog: BsModalRef;
    createOrEditUserDialog = this._modalService.show(
      CreateUserDialogComponent,
      {
        class: 'modal-lg',
      }
    );
    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  showEditModal(id: number): void {
    let EditUserDialog = this._modalService.show(
      EditUserDialogComponent,
      {
        class: 'modal-lg',
        initialState: {
          id: id,
        },
      }
    );
    EditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });

  }

  protected delete(entity: UserDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', this.selected.length, 'Users'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._userService.delete(entity.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  deleteItem(): void {
    if (this.selected.length == 0) {
      abp.message.info(this.l('YouHaveToSelectOneItemInMinimum'));
    }
    else {
      abp.message.confirm(
        this.l('UserDeleteWarningMessage', this.selected.length, 'Users'),
        undefined,
        (result: boolean) => {
          if (result) {
            this.selected.forEach(element => {
              this._userService.delete(element.id).subscribe(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              });
            });
          }
        }
      );
    }
  }

  loadData(pageSize: number = 10, currentPage: number = 1, search: string = '', orderBy: string = ''): void {
    let request: PagedUsersRequestDto = new PagedUsersRequestDto();
    search = '';
    this.itemsPerPage = pageSize;
    this.currentPage = currentPage;
    this.search = search;
    request.keyword = search;
    request.orderBy = orderBy;
    request.isActive = this.isActive;
    request.skipCount = (currentPage - 1) * pageSize;
    request.maxResultCount = this.itemsPerPage;
    this.list(request, this.pageNumber, () => { });
  }

  searchKeyUp(event): void {
    const val = event.target.value.toLowerCase().trim();
    this.loadData(this.itemsPerPage, 1, val, this.itemOrder.value);
  }

  onContextMenuClick(action: string, item: UserDto): void {
    switch (action) {
      case "delete":
        this.delete(item);
        break;
      case "edit":
        this.showEditModal(item.id);
        break;
      default:
        break;
    }
  }

  itemsPerPageChange(perPage: number): void {
    this.loadData(perPage, 1, this.search, this.itemOrder.value);
  }

  isSelected(p: UserDto): boolean {
    return this.selected.findIndex(x => x.id === p.id) > -1;
  }

  onSelect(item: UserDto): void {
    if (this.isSelected(item)) {
      this.selected = this.selected.filter(x => x.id !== item.id);
    } else {
      this.selected.push(item);
    }
    this.setSelectAllState();
    this.selectedCount = this.selected.length;
  }

  setSelectAllState(): void {
    if (this.selected.length === this.data.length) {
      this.selectAllState = 'checked';
    } else if (this.selected.length !== 0) {
      this.selectAllState = 'indeterminate';
    } else {
      this.selectAllState = '';
    }
  }

  selectAllChange($event): void {
    if ($event.target.checked) {
      this.selected = [...this.data];
    } else {
      this.selected = [];
    }
    this.setSelectAllState();
  }

  public resetPassword(user: UserDto): void {
    this.showResetPasswordUserDialog(user.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.search;
    request.isActive = this.isActive;

    this._userService
      .getAll(
        request.keyword,
        request.orderBy,
        request.isActive,
        request.skipCount,
        request.maxResultCount,
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: UserDtoPagedResultDto) => {
        this.users = result.items;
        this.data = result.items;
        this.setSelectAllState();
        this.showPaging(result, pageNumber);
      });
  }

  private showResetPasswordUserDialog(id?: number): void {
    this._modalService.show(ResetPasswordDialogComponent, {
      class: 'modal-lg',
      initialState: {
        id: id,
      },
    });
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateUserDialogComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditUserDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
