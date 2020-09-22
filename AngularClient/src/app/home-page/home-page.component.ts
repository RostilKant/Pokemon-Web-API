import { Component, OnInit } from '@angular/core';
import {delay} from 'rxjs/operators';
import {PokemonService} from '../admin/shared/services/pokemon.service';


@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  constructor(private pokemonService: PokemonService) { }
  poke;
  isLoading;

  ngOnInit(): void {
    this.isLoading = true;
    this.pokemonService.getAllFromPokeApi()
      .pipe(delay(2000))
      .subscribe((response: any) => {
        this.poke = response.results;
        this.isLoading = false;
      }, error => {
        console.log(error);
      });
  }

}
