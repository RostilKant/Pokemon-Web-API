import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {delay} from 'rxjs/operators';

const img = 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/' ;

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  constructor(private http: HttpClient) { }
  poke;
  isLoading;

  ngOnInit(): void {
    this.isLoading = true;
    this.http.get('https://pokemon-web-api.azurewebsites.net/api/pokemons/poke-api')
      .pipe(delay(2000))
      .subscribe((response: any) => {
        this.poke = response.results;
        this.isLoading = false;
      }, error => {
        console.log(error);
      });
  }

  generateSrc(id: number): string {
    return img + `${id}.png`;
  }

}
