import { Component, Injector, ViewChild } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Hotkey, HotkeysService } from 'angular2-hotkeys';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto,
  PagedResultDto
} from 'shared/paged-listing-component-base';
import {
  EmployeeServiceProxy,
  ReadEmployeeDto,
  ReadEmployeeDtoPagedResultDto,
} from '@shared/service-proxies/service-proxies';
import { CreateEmployeeDialogComponent } from './create-employee/create-employee-dialog.component';
import { EditEmployeeDialogComponent } from './edit-employee/edit-employee-dialog.component';
import { ContextMenuComponent } from 'ngx-contextmenu';


class PagedEmployeesRequestDto extends PagedRequestDto {
  keyword: string;
  orderBy: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './employee.component.html',
  animations: [appModuleAnimation()]
})
export class EmployeeComponent extends PagedListingComponentBase<ReadEmployeeDto> {
  employees: ReadEmployeeDto[] = [];
  title = "Employees"
  displayMode = 'list';
  itemOrder = { label: this.l("FullName"), value: "fullName" };
  itemOptionsOrders = [{ label: this.l("FullName"), value: "fullName" },
  { label: this.l("Employeename"), value: "employeeName" }];
  itemsPerPage = 10;
  selectAllState = '';
  selected: ReadEmployeeDto[] = [];
  data: ReadEmployeeDto[] = [];
  currentPage = 1;
  search = '';
  selectedCount = 0;
  isActive: boolean | null = true;
  advancedFiltersVisible = false;

  @ViewChild('basicMenu') public basicMenu: ContextMenuComponent;
  @ViewChild('addNewModalRef', { static: true }) addNewModalRef: CreateEmployeeDialogComponent;

  constructor(
    private hotkeysService: HotkeysService,
    injector: Injector,
    private _employeeService: EmployeeServiceProxy,
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
    let createOrEditEmployeeDialog: BsModalRef;
    createOrEditEmployeeDialog = this._modalService.show(
      CreateEmployeeDialogComponent,
      {
        class: 'modal-lg',
      }
    );
    createOrEditEmployeeDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  showEditModal(id:number): void {
    
      let EditEmployeeDialog = this._modalService.show(
        EditEmployeeDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
      EditEmployeeDialog.content.onSave.subscribe(() => {
        this.refresh();
      });
    
  }

  protected delete(entity: ReadEmployeeDto): void {
    abp.message.confirm(
      this.l('EmployeeDeleteWarningMessage', this.selected.length, 'Employees'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._employeeService.delete(entity.id).subscribe(() => {
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
        this.l('EmployeeDeleteWarningMessage', this.selected.length, 'Employees'),
        undefined,
        (result: boolean) => {
          if (result) {
            this.selected.forEach(element => {
              this._employeeService.delete(element.id).subscribe(() => {
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
    let request: PagedEmployeesRequestDto = new PagedEmployeesRequestDto();
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

  onContextMenuClick(action: string, item: ReadEmployeeDto): void {
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

  isSelected(p: ReadEmployeeDto): boolean {
    return this.selected.findIndex(x => x.id === p.id) > -1;
  }

  onSelect(item: ReadEmployeeDto): void {
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

  clearFilters(): void {
    this.search = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedEmployeesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.search;
    request.isActive = this.isActive;

    this._employeeService
      .getAllWithInput(
        request.keyword,
        request.orderBy,
        request.skipCount,
        request.maxResultCount,
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ReadEmployeeDtoPagedResultDto) => {
        this.employees = result.items;
        this.data = result.items;
        this.setSelectAllState();
        this.showPaging(result, pageNumber);
      });
  }
}
