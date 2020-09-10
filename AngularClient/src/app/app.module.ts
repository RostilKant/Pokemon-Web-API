import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {SharedModule} from './shared/shared.module';
import { HomePageComponent } from './home-page/home-page.component';
import { PokemonPageComponent } from './pokemon-page/pokemon-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    PokemonPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
