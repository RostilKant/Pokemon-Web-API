export interface User {
  userName: string;
  password: string;
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
}
