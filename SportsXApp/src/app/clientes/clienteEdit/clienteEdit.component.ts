import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from 'src/app/_models/Cliente';
import { ClienteService } from 'src/app/_services/cliente.service';

@Component({
  selector: 'app-clienteEdit',
  templateUrl: './clienteEdit.component.html',
  styleUrls: ['./clienteEdit.component.css']
})

export class ClienteEditComponent implements OnInit {

  cliente: Cliente;
  clientes: Cliente[];
  clientesFiltrados: Cliente[];

  _filtroLista: string;
  registerForm: FormGroup;
  
  id = 0;
 
  get telefones(): FormArray{
    return <FormArray>this.registerForm.get('telefones');
  }

  constructor(
    private clienteService : ClienteService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
  ) { }

  ngOnInit() {
    this.validation();
    this.carregarCliente();
  }

  carregarCliente() {

    if(!this.route.snapshot.paramMap.has('id')){
        this.id = 0;
        return;
    }

    if(!Number(this.route.snapshot.paramMap.get('id')) || Number(this.route.snapshot.paramMap.get('id')) <= 0) {
      this.id = 0;
      this.toastr.warning("Não foi possível recuperar o Cliente",'Editar');
      return;
    }

    this.id = parseInt(this.route.snapshot.paramMap.get('id'));
    
    if(this.id > 0) 
    {
        this.clienteService.getClienteById(this.id).subscribe((cliente: Cliente) => {
          if(cliente != null){
            this.cliente = Object.assign({},cliente);
            this.registerForm.patchValue(cliente);
    
            this.cliente.telefones.forEach(telefone =>{
                this.telefones.push(this.criarTelefone(telefone));
              })
          }
          else{
              this.toastr.warning("Não foi possível recuperar o Cliente",'Editar');
              this.id = 0;
          }
        }, error => {
          this.toastr.error("Não foi possível recuperar o Cliente",'Editar');
        });
    }
  }

  validation(){
    this.registerForm = this.formBuilder.group({
      id:[0],
      tipo: ['',Validators.required],
      classificacao: ['',Validators.required],
      nome: ['',[Validators.required,Validators.maxLength(100)]],
      razaoSocial: ['',Validators.maxLength(100)],
      cpf: ['',Validators.minLength(11)],
      cnpj: ['',Validators.minLength(14)],
      cep: ['',Validators.minLength(8)],
      email: ['',[Validators.required,Validators.email]],
      telefones: this.formBuilder.array([])
    });
  }

  criarTelefone(telefone: any): FormGroup {
    return this.formBuilder.group({
        id: [telefone.id],
        numero: [telefone.numero,[Validators.required,Validators.minLength(11)]]
     });
  }

  adicionarTelefone(){
    this.telefones.push(this.criarTelefone({ id: 0, numero: '' }));
  }

  removerTelefone(id:number){
    this.telefones.removeAt(id);
  }

  salvarCliente(){
    if(this.registerForm.valid)
    {
      if(this.id == 0)
      {
        this.cliente = Object.assign({},this.registerForm.value);
        this.clienteService.postCliente(this.cliente).subscribe(
          (novoCliente:Cliente) =>{
            this.toastr.success("Salvo com sucesso!",'Salvar');
            this.id = novoCliente.id;
            this.router.navigate([ 'cliente/',this.id, 'edit']);
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
          }, error => {
            this.toastr.error("Erro ao Editar",'Editar');
          }
        );
      }
    }
  }
}
