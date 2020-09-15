import { Component, OnInit } from '@angular/core';
import {PokemonService} from '../shared/services/pokemon.service';
import {delay} from 'rxjs/operators';
import {PokemonDto} from '../../shared/interfaces';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

  isLoading;
  pokemons: PokemonDto[];
  displayedColumns: string[] = ['id', 'name', 'height', 'weight'];

  constructor(private pokemonService: PokemonService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.pokemonService.getAllPokemons()
      .pipe(delay(2000))
      .subscribe((response: any) => {
        this.pokemons = response;
        this.isLoading = false;
      }, error => {
        console.log(error);
      });
  }

}
