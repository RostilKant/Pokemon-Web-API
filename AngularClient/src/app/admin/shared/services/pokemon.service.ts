import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Pokemon} from '../../../shared/interfaces';


@Injectable()
export class PokemonService {

  constructor(private http: HttpClient) { }

  create(pokemon: Pokemon): Observable<Pokemon> {
    return this.http.post<Pokemon>('https://localhost:5001/api/pokemons', pokemon);
  }

  getAllFromPokeApi(): Observable<any> {
    return this.http.get('https://localhost:5001/api/pokemons/poke-api');
  }
  getByIdFromPokeApi(id: number): Observable<any> {
    return this.http.get(`https://localhost:5001/api/pokemons/poke-api/${id}`);
  }

  getAll(): Observable<Pokemon[]> {
    return this.http.get<Pokemon[]>('https://localhost:5001/api/pokemons');
  }

  getById(id: number): Observable<Pokemon> {
    return this.http.get<Pokemon>(`https://localhost:5001/api/pokemons/${id}`);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`https://localhost:5001/api/pokemons/${id}`);
  }

  update(pokemon: Pokemon): Observable<Pokemon> {
    return this.http.put<Pokemon>(`https://localhost:5001/api/pokemons/${pokemon.id}`, pokemon);
  }
}
