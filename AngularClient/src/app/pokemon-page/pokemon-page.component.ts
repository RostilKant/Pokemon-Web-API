import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {PokemonService} from '../admin/shared/services/pokemon.service';
import {Observable} from 'rxjs';
import {Pokemon, Sprite} from '../shared/interfaces';
import {switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-pokemon-page',
  templateUrl: './pokemon-page.component.html',
  styleUrls: ['./pokemon-page.component.scss']
})
export class PokemonPageComponent implements OnInit {

  pokemon$: Observable<Pokemon>;

  constructor(
    private route: ActivatedRoute,
    private pokemonService: PokemonService
  ) { }

  ngOnInit(): void {
    this.pokemon$ = this.route.params.pipe(
      switchMap((params: Params) => {
        return this.pokemonService.getByIdFromPokeApi(params.id);
      })
    );
  }

  printIt(types): string[] {
    const stringTypes: string[] = [];
    types.forEach((type) => {
      stringTypes.push(type.type.name.charAt(0).toUpperCase() + type.type.name.slice(1));
    });
    return stringTypes;
  }

  frontSprite(sprites: Sprite[]): string {
    let frontDefault = '';

    sprites.find(s => {
      frontDefault = s.front_default;
    });
    return frontDefault;
  }
  backSprite(sprites: Sprite[]): string {
    let backDefault = '';

    sprites.find(s => {
      backDefault = s.back_default;
    });
    return backDefault;
  }
}
