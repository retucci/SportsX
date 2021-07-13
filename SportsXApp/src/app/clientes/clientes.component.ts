import { templateJitUrl } from '@angular/compiler';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
  modoSalvar = "post";
  exibirDocumento = "";
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
    this.validation();
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
    this.registerForm.reset();
    template.show();
  }

  editarCliente(cliente: Cliente, template:any){
    this.modoSalvar = "put";
    this.openModal(template);
    this.cliente = cliente;
    this.registerForm.patchValue(cliente);
  }

  novoCliente(template:any){
    this.modoSalvar = "post";
    this.openModal(template);
  }

  validation(){
    this.registerForm = this.formBuilder.group({
      tipo: ['',Validators.required],
      classificacao: ['',Validators.required],
      nome: ['',[Validators.required,Validators.maxLength(100)]],
      razaoSocial: ['',Validators.maxLength(100)],
      cpf: ['',Validators.maxLength(11)],
      cnpj: ['',Validators.maxLength(14)],
      cep: ['',Validators.maxLength(8)],
      email: ['',[Validators.required,Validators.email]]
    });
  }

  salvarAlteracao(template:any){
    if(this.registerForm.valid)
    {
      if(this.modoSalvar === 'post')
      {
        this.cliente = Object.assign({},this.registerForm.value);
        this.clienteService.postCliente(this.cliente).subscribe(
          (novoCliente:Cliente) =>{
            template.hide();
            this.getClientes();
            this.toastr.success("Salvo com sucesso!",'Salvar');
          }, error => {
            this.toastr.error("Erro ao Salvar",'Salvar');
          }
        );
      }
      else
      {
        this.cliente = Object.assign({id:this.cliente.id},this.registerForm.value);
        this.clienteService.putCliente(this.cliente).subscribe(
          (novoCliente:Cliente) =>{
            this.toastr.success("Editado com sucesso!",'Editar');
            template.hide();
            this.getClientes();
          }, error => {
            this.toastr.error("Erro ao Editar",'Editar');
          }
        );
      }
    }
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
