import {Pipe, PipeTransform} from '@angular/core';
import {PokemonDto} from '../interfaces';


@Pipe({
  name: 'searchType'
})
export class SearchByTypePipe implements PipeTransform {
  transform(pokemons: PokemonDto[], search = ''): PokemonDto[] {
    if (!search.trim()) {
      return pokemons;
    }

    return  pokemons.filter(pokemon => {
      return  pokemon.types.find(type => {
        return type.name.toLowerCase().includes(search.toLowerCase());
      });
    });
  }

}
