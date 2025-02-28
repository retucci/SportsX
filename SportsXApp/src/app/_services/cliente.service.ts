import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../_models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

constructor(private http: HttpClient) { }

  baseURL = 'http://localhost:5000/api/cliente';
  
  getCliente(): Observable<Cliente[]>{
      return this.http.get<Cliente[]>(this.baseURL);
  }

  getClienteByName(name:string): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.baseURL}/getByName/${name}`);
  }

  getClienteById(id:number): Observable<Cliente>{
    return this.http.get<Cliente>(`${this.baseURL}/${id}`);
  }

  postCliente(cliente:Cliente){
    return this.http.post(this.baseURL,cliente);
  }

  putCliente(cliente:Cliente){
    return this.http.put(`${this.baseURL}/${cliente.id}`,cliente);
  }

  deleteCliente(id: number){
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
