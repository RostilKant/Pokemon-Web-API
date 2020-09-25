import {Pipe, PipeTransform} from '@angular/core';
import {Pokemon} from '../interfaces';

@Pipe({
  name: 'searchName'
})
export class SearchByNamePipe implements PipeTransform{
  transform(pokemons: Pokemon[], search = ''): Pokemon[] {
    if (!search.trim()) {
      return pokemons;
    }
    return pokemons.filter(pokemon => {
      return pokemon.name.toLocaleLowerCase().includes(search.toLocaleLowerCase());
    });
  }

}
