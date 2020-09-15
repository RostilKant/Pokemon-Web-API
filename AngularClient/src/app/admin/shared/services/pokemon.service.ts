import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PokemonDto} from '../../../shared/interfaces';


@Injectable()
export class PokemonService {

  constructor(private http: HttpClient) { }

  create(pokemon: PokemonDto): Observable<PokemonDto> {
    return this.http.post<PokemonDto>('https://pokemon-web-api.azurewebsites.net/api/pokemons', pokemon);
  }

  getAllFromPokeApi(): Observable<any> {
    return this.http.get('https://pokemon-web-api.azurewebsites.net/api/pokemons/poke-api');
  }

  getAllPokemons(): Observable<PokemonDto[]> {
    return this.http.get<PokemonDto[]>('https://pokemon-web-api.azurewebsites.net/api/pokemons', {
      params: {
        PageSize: '20'
      }
    });
  }
}
