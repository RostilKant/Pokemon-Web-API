export interface LoginUser {
  userName: string;
  password: string;
}

export interface RegistrationUser {
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
  email: string;
  phoneNumber: string;
  roles: string[];
}

export interface MyToken {
  token: string;
  expiresIn: string;
}

export interface Type {
  id?: number;
  name: string;
}

export interface PokemonDto {
  id?: number;
  name: string;
  height: number;
  weight: number;
  types: Type[];
  sprites?: Sprite[];
}

export interface Sprite {
  front_default: string;
  back_default: string;
}


