import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PokemonDto} from '../../../shared/interfaces';


@Injectable()
export class PostService {

  constructor(private http: HttpClient) { }

  create(pokemon: PokemonDto): Observable<PokemonDto> {
    return this.http.post<PokemonDto>('https://pokemon-web-api.azurewebsites.net/api/pokemons', pokemon);
   /* {
      headers: {
        Authorization: localStorage.getItem('jwt-token'),
      }
    });*/
  }
}
