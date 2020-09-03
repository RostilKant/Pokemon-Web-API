import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

  constructor(private http: HttpClient) { }
  poke;

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/pokemons/poke-api').subscribe((response: any) => {
      this.poke = response.results;
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  generateSrc(id: number): string {
    return `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${id}.png`;
  }
}
