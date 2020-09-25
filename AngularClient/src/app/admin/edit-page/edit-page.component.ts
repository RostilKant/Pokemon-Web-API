import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {PokemonService} from '../shared/services/pokemon.service';
import {switchMap} from 'rxjs/operators';
import {Pokemon, Type} from '../../shared/interfaces';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-edit-page',
  templateUrl: './edit-page.component.html',
  styleUrls: ['./edit-page.component.scss']
})
export class EditPageComponent implements OnInit, OnDestroy {

  form: FormGroup;
  disable = false;
  uSub: Subscription;
  pokemon: Pokemon;

  constructor(
    private route: ActivatedRoute,
    private pokemonService: PokemonService,
    private builder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.form = this.builder.group({
      name: [null, [Validators.required]],
      height: [null, [Validators.required, Validators.min(18)]],
      weight: [null, [Validators.required, Validators.min(18)]],
      types: this.builder.array([])
    });
    this.route.params.pipe(
        switchMap((params: Params) => {
          return this.pokemonService.getById(params.id);
        })
    ).subscribe( (pokemon: Pokemon) => {
      this.pokemon = pokemon;
      this.form.patchValue({
        name: pokemon.name,
        height: pokemon.height,
        weight: pokemon.weight
      });
      for (const type of pokemon.types){
        (this.form.get('types') as FormArray).push(new FormControl(type.name, Validators.required));
      }
    });
  }

  ngOnDestroy(): void {
    if (this.uSub){
      this.uSub.unsubscribe();
    }
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

    this.uSub = this.pokemonService.update({
      id: this.pokemon.id,
      name: this.form.value.name,
      height: this.form.value.height,
      weight: this.form.value.weight,
      types: allTypes
    }).subscribe(() => {
      this.disable = false;
    });
  }
}
