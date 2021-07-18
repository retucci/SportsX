import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './clientes/clientes.component';
import { ClienteEditComponent } from './clientes/clienteEdit/clienteEdit.component';

const routes: Routes = [
  {path: 'clientes',component: ClientesComponent},
  {path: 'cliente/:id/edit',component: ClienteEditComponent},
  {path: 'cliente',component: ClienteEditComponent},
  {path: '', redirectTo:'clientes', pathMatch:'full'},
  {path: '**', redirectTo:'clientes', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
