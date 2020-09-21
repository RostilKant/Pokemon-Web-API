import {Pipe, PipeTransform} from '@angular/core';
import {PokemonDto} from '../interfaces';

@Pipe({
  name: 'searchName'
})
export class SearchByNamePipe implements PipeTransform{
  transform(pokemons: PokemonDto[], search = ''): PokemonDto[] {
    if (!search.trim()) {
      return pokemons;
    }
    return pokemons.filter(pokemon => {
      return pokemon.name.toLocaleLowerCase().includes(search.toLocaleLowerCase());
    });
  }

}
