import {Component, OnInit} from '@angular/core';
import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';
import {Pokemon, Type} from '../../shared/interfaces';
import {PokemonService} from '../shared/services/pokemon.service';
// import {COMMA, ENTER} from '@angular/cdk/keycodes';

@Component({
  selector: 'app-create-page',
  templateUrl: './create-page.component.html',
  styleUrls: ['./create-page.component.scss']
})
export class CreatePageComponent implements OnInit {

  form: FormGroup;
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

    const pokemon: Pokemon = {
      name: this.form.value.name,
      height: this.form.value.height,
      weight: this.form.value.weight,
      types: allTypes
    };


    this.postService.create(pokemon).subscribe(() => {
      console.log(pokemon.types.forEach(console.log));
    });

  }
}
