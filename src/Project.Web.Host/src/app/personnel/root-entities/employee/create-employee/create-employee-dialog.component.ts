import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  EmployeeServiceProxy,
  CreateEmployeeDto,
  NationalityServiceProxy,
  ListViewDto,
  Gender,
} from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';

@Component({
  templateUrl: './create-employee-dialog.component.html'
})
export class CreateEmployeeDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  gender:String;nationalityId:String;
  genders :Gender[] = [Gender.Female,Gender.Male]
  employee = new CreateEmployeeDto();
  nationalities : ListViewDto[] = [];
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _employeeService: EmployeeServiceProxy,
    public _nationalityService: NationalityServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._nationalityService.getNationalitiesLookUp().subscribe((result) => {
      this.nationalities = result;
    });
  }

  save(): void {
    this.saving = true;
    console.log(this.gender);
    console.log(this.nationalityId);
    this._employeeService.insert(this.employee).subscribe(
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
