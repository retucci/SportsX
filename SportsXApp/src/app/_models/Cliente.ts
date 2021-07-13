import { ClienteTelefone } from './ClienteTelefone';

export interface Cliente {
    id: number;
    tipo: number;
    nome: string;
    razaoSocial: string;
    cpf: string;
    cnpj: string;
    cep: string;
    classificacao: number;
    email: string;
    telefones: ClienteTelefone[];
}
