import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PokemonDto} from '../../../shared/interfaces';


@Injectable()
export class PokemonService {

  constructor(private http: HttpClient) { }

  create(pokemon: PokemonDto): Observable<PokemonDto> {
    return this.http.post<PokemonDto>('https://localhost:5001/api/pokemons', pokemon);
  }

  getAllFromPokeApi(): Observable<any> {
    return this.http.get('https://localhost:5001/api/pokemons/poke-api');
  }

  getAllPokemons(): Observable<PokemonDto[]> {
    return this.http.get<PokemonDto[]>('https://localhost:5001/api/pokemons');
  }

  getPokemonById(id: number): Observable<PokemonDto> {
    return this.http.get<PokemonDto>(`https://localhost:5001/api/pokemons/${id}`);
  }

  deletePokemon(pokemonId: number): Observable<void> {
    return this.http.delete<void>(`https://localhost:5001/api/pokemons/${pokemonId}`);
  }
}
