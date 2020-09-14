import {Component, OnInit} from '@angular/core';
import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';
import {Type} from '../../shared/interfaces';

@Component({
  selector: 'app-create-page',
  templateUrl: './create-page.component.html',
  styleUrls: ['./create-page.component.scss']
})
export class CreatePageComponent implements OnInit {

  /*['poison', 'ground', 'rock', 'ghost', 'fire', 'steel', 'water', 'grass',
    'electric', 'psychic', 'ice', 'dragon', 'shadow' ];*/

  types: Type[] = [
    /*{name: 'poison'},
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
    {name: 'unknown'}*/
  ];

  form: FormGroup;

  constructor() {
  }

  ngOnInit(): void {
    this.form = new FormGroup({
        name: new FormControl(null, Validators.required),
        height: new FormControl(null, [Validators.required, Validators.min(1)]),
        weight: new FormControl(null, [Validators.required, Validators.min(1)]),
        types: new FormArray([])
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

}
