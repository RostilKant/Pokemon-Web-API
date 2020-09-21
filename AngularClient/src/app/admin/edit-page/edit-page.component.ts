import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {PokemonService} from '../shared/services/pokemon.service';
import {switchMap} from 'rxjs/operators';
import {PokemonDto, Type} from '../../shared/interfaces';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-edit-page',
  templateUrl: './edit-page.component.html',
  styleUrls: ['./edit-page.component.scss']
})
export class EditPageComponent implements OnInit {

  form: FormGroup;
  disable = false;

  constructor(
    private route: ActivatedRoute,
    private pokemonService: PokemonService,
    private builder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.route.params.pipe(
        switchMap((params: Params) => {
          return this.pokemonService.getPokemonById(params.id);
        })
    ).subscribe( (pokemon: PokemonDto) => {
      this.form = this.builder.group({
        name: [pokemon.name, [Validators.required]],
        height: [pokemon.height, [Validators.required, Validators.min(18)]],
        weight: [pokemon.weight, [Validators.required, Validators.min(18)]],
        types: this.builder.array([])
      });
      for (const type of pokemon.types){
        (this.form.get('types') as FormArray).push(new FormControl(type.name, Validators.required));
      }
    });
  }

  addType(): void {
    const control = new FormControl('', Validators.required);
    (this.form.get('types') as FormArray).push(control);
  }

  getErrorMessage(control: string): string {
    if (this.form.get(control.toLocaleLowerCase()).errors.required) {
      return `${control} is required`;
    }
    if (this.form.get(control.toLocaleLowerCase()).errors.min) {
      return `${control} minimum is ${this.form.get(control.toLocaleLowerCase()).errors.min.min}`;
    }
  }


  submit(): void {

    if (this.form.invalid) {
      return;
    }
    this.disable = true;

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

   /* this.postService.create(pokemon).subscribe(() => {
      console.log(pokemon.types.forEach(console.log));
    });*/
    this.disable = false;
  }
}
