import {Component, OnInit} from '@angular/core';
import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';
import {PokemonDto, Type} from '../../shared/interfaces';
import {PokemonService} from '../shared/services/pokemon.service';
// import {COMMA, ENTER} from '@angular/cdk/keycodes';

@Component({
  selector: 'app-create-page',
  templateUrl: './create-page.component.html',
  styleUrls: ['./create-page.component.scss']
})
export class CreatePageComponent implements OnInit {
  // readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  /*['poison', 'ground', 'rock', 'ghost', 'fire', 'steel', 'water', 'grass',
    'electric', 'psychic', 'ice', 'dragon', 'shadow' ];*/
  jsonPokemon: PokemonDto;
/*
  types: Type[] = [
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
  ];
*/

  form: FormGroup;
  // removable = true;
  disable = false;

  constructor(private postService: PokemonService) {
  }

  ngOnInit(): void {
    this.form = new FormGroup({
        name: new FormControl(null, [Validators.required]),
        height: new FormControl(null, [Validators.required, Validators.min(18)]),
        weight: new FormControl(null, [Validators.required, Validators.min(18)]),
        types: new FormArray([

        ])
      });
  }

  getErrorMessage(control: string): string {
    if (this.form.get(control.toLocaleLowerCase()).errors.required) {
      return `${control} is required`;
    }
    if (this.form.get(control.toLocaleLowerCase()).errors.min) {
      return `${control} minimum is ${this.form.get(control.toLocaleLowerCase()).errors.min.min}`;
    }
  }

  addType(): void {
    const control = new FormControl('', Validators.required);
    (this.form.get('types') as FormArray).push(control);
  }

  submit(): void {
    if (this.form.invalid) {
      return;
    }
    this.disable = true;

    setTimeout(() => { this.form.reset(); this.disable = false; }, 1000);

    const types = this.form.value.types;
    const allTypes: Type[] = [];

    // tslint:disable-next-line:prefer-for-of forin
    for (const i in types) {
      allTypes.push({
        name: types[+i]
      });
    }

    const pokemon: PokemonDto = {
      name: this.form.value.name,
      height: this.form.value.height,
      weight: this.form.value.weight,
      types: allTypes
    };

    this.jsonPokemon = pokemon;

    this.postService.create(pokemon).subscribe(() => {
      console.log(pokemon.types.forEach(console.log));
    });

  }

 /* add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      this.types.push({name: value.trim()});
      this.addType();
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(type: Type): void {
    const index = this.types.indexOf(type);

    if (index >= 0) {
      this.types.splice(index, 1);
    }
  }*/
}
