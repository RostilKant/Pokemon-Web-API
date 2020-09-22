import {Component, OnDestroy, OnInit} from '@angular/core';
import {PokemonService} from '../shared/services/pokemon.service';
import {PokemonDto, Type} from '../../shared/interfaces';
import {Subscription} from 'rxjs';
import {delay} from 'rxjs/operators';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit, OnDestroy{

  isLoading;
  pokemons: PokemonDto[];
  displayedColumns: string[] = ['id', 'name', 'height', 'weight', 'types', 'actions'];
  searchByName = '';
   types = ['poison', 'ground', 'rock', 'ghost', 'fire', 'steel', 'water', 'grass',
    'electric', 'psychic', 'ice', 'dragon', 'shadow' ];
  searchByType = '';
  /*types: Type[] = [
    {name: 'poison'},
    {name: 'ground'},
    {name: 'rock'},
    {name: 'ghost'},
    {name: 'fire'},
    {name: 'steel'},
    {name: 'water'},
    {name: 'grass'},
    {name: 'electric'},
    {name: 'psychic'},
    {name: 'ice'},
    {name: 'dragon'},
    {name: 'dark'},
    {name: 'shadow'},
    {name: 'fairy'},
    {name: 'unknown'}
  ];*/

  getAllSub: Subscription;
  deletePokemonSub: Subscription;

  constructor(private pokemonService: PokemonService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.getAllSub = this.pokemonService.getAll()
      .pipe(delay(2000))
      .subscribe((response: PokemonDto[]) => {
        this.pokemons = response;
        console.log(response);
        this.isLoading = false;
      }, error => {
        console.log(error);
      });
  }

  remove(id: number): void {
    this.deletePokemonSub = this.pokemonService.delete(id)
      .subscribe(() => {
        this.pokemons = this.pokemons.filter( p => p.id !== id);
      });
  }

  ngOnDestroy(): void {
    if (this.getAllSub){
      this.getAllSub.unsubscribe();
    }

    if (this.deletePokemonSub){
      this.getAllSub.unsubscribe();
    }
  }

  printIt(types: Type[]): string[] {
    const stringTypes: string[] = [];
    types.forEach((type) => {
      stringTypes.push(type.name.charAt(0).toUpperCase() + type.name.slice(1));
    });
    return stringTypes;
  }
}
