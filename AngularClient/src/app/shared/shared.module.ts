import {NgModule} from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {HttpClientModule} from '@angular/common/http';
import {MatListModule} from '@angular/material/list';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { PokemonComponent } from './components/pokemon/pokemon.component';
import {MainLayoutComponent} from './components/main-layout/main-layout.component';
import {RouterModule} from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatChipsModule} from '@angular/material/chips';
import {MatTableModule} from '@angular/material/table';
import {FormsModule} from '@angular/forms';
import {PokemonService} from '../admin/shared/services/pokemon.service';
import {SearchByNamePipe} from './pipes/searchByName.pipe';
import {SearchByTypePipe} from './pipes/searchByType.pipe';
import {MatSelectModule} from '@angular/material/select';


@NgModule({
  declarations: [
    PokemonComponent,
    MainLayoutComponent,
    SearchByNamePipe,
    SearchByTypePipe
  ],
  imports: [
    HttpClientModule,
    RouterModule,
    FormsModule,

    MatToolbarModule,
    MatIconModule
  ],
  exports: [
    HttpClientModule,
    RouterModule,
    SearchByNamePipe,
    SearchByTypePipe,

    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatCheckboxModule,
    MatAutocompleteModule,
    MatChipsModule,
    MatTableModule,
    MatSelectModule,
    FormsModule,

    PokemonComponent
  ],
  providers: [
    PokemonService
  ]
})
export class SharedModule{ }
