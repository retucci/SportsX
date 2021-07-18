import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Cliente } from '../_models/Cliente';
import { ClienteService } from '../_services/cliente.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})

export class ClientesComponent implements OnInit {

  cliente: Cliente;
  clientes: Cliente[];
  clientesFiltrados: Cliente[];

  _filtroLista: string;
  registerForm: FormGroup;
  msgDeletarCliente = "";

  constructor(
    private clienteService : ClienteService,
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  ) { }

  get filtroLista(): string {
    return this._filtroLista;
  }
  
  set filtroLista(value: string){
    this._filtroLista = value;
    this.clientesFiltrados = this.filtroLista ? this.filterClientes(this.filtroLista) : this.clientes;
  }

  ngOnInit() {
    this.getClientes();
  }

  filterClientes(filtrarPor: string): Cliente[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.clientes.filter(
      cliente => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  getClientes(){
    this.clienteService.getCliente().subscribe((_clientes: Cliente[]) => {
      this.clientes = _clientes;
      this.clientesFiltrados = _clientes;
    }, error => { console.log(error)})
  }
  
  openModal(template: any){
    template.show();
  }
  
  excluirCliente(cliente: Cliente, template: any) {
    this.openModal(template);
    this.cliente = cliente;
    this.msgDeletarCliente = `Tem certeza que deseja excluir o Cliente: ${cliente.nome}`;
  }
  
  confirmeDelete(template: any) {
    this.clienteService.deleteCliente(this.cliente.id).subscribe(
      () => {
          template.hide();
          this.getClientes();
          this.toastr.success("Excluído com sucesso!",'Excluir');
        }, error => {
          this.toastr.error("Erro ao Excluir",'Excluir');
        }
    );
  }
}
