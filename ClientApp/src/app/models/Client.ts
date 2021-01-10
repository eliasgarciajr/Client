export class Client {
  id: number;
  name: string;
  lastName: string;
  email: string;
  dateBirth: string;
  password: string;
  eEducationType: number;
  phones: [{id: number; numberPhone: string; clienteId: number}];
}
