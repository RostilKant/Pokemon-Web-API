import {Pipe, PipeTransform} from '@angular/core';
import {Pokemon} from '../interfaces';


@Pipe({
  name: 'searchType'
})
export class SearchByTypePipe implements PipeTransform {
  transform(pokemons: Pokemon[], search = ''): Pokemon[] {
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
