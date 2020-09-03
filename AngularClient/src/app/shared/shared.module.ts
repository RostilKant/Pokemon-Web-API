import {NgModule} from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {HttpClientModule} from '@angular/common/http';
import {MatListModule} from '@angular/material/list';


@NgModule({
  imports: [
    HttpClientModule
  ],
  exports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    HttpClientModule
  ]
})
export class SharedModule{ }
