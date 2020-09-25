import {Component, Input, OnInit} from '@angular/core';
import {Pokemon} from '../../interfaces';

const img = 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/' ;

@Component({
  selector: 'app-pokemon',
  templateUrl: './pokemon.component.html',
  styleUrls: ['./pokemon.component.scss']
})
export class PokemonComponent implements OnInit {

  @Input() pokemonApi;
  @Input() pokemonId: number;

  pokemon: Pokemon;

  constructor() { }

  ngOnInit(): void {}

  generateSrc(id: number): string {
    return img + `${id + 1}.png`;
  }

}
