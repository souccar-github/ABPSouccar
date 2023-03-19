import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  EmployeeServiceProxy,
  EmployeeDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './edit-employee-dialog.component.html'
})
export class EditEmployeeDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  employee = new EmployeeDto();
  checkedRolesMap: { [key: string]: boolean } = {};
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _employeeService: EmployeeServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._employeeService.get(this.id).subscribe((result) => {
      this.employee = result;
    });
  }

  save(): void {
    this.saving = true;

    this._employeeService.update(this.employee).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
