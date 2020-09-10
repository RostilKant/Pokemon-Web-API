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


@NgModule({
  declarations: [
    PokemonComponent,
    MainLayoutComponent
  ],
  imports: [
    HttpClientModule,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatProgressSpinnerModule,
  ],
  exports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    RouterModule,
    PokemonComponent
  ]
})
export class SharedModule{ }
