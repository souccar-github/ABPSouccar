import { DragDropModule } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StateButtonComponent } from './state-button/state-button.component';
import { TwoListDragAndDropComponent } from './two-lists-drag-and-drop/two_list_drag_and_drop.component';



@NgModule({
  declarations: [
    StateButtonComponent,
    TwoListDragAndDropComponent
  ],
  imports: [
    CommonModule,
    DragDropModule,
    FormsModule,
    
    
  ],
  providers: [
  ],
  schemas: [
    NO_ERRORS_SCHEMA
  ],
  exports:[
    StateButtonComponent,
    TwoListDragAndDropComponent
  ]
})
export class WidgetsModule {}
