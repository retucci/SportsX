using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsXWebAPI.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O Tipo é obrigatório")]
        public int Tipo { get; set; }

        [Required(ErrorMessage ="O Nome é obrigatório")]
        [StringLength(100, ErrorMessage ="O campo Nome não pode exceder 100 caracteres")]
        public string Nome { get; set; }

        [StringLength(100, ErrorMessage ="O campo Razão Social não pode exceder 100 caracteres")]
        public string RazaoSocial { get; set; }

        [StringLength(11, ErrorMessage ="O campo CPF não pode exceder 8 caracteres")]
        public string Cpf { get; set; }

        [StringLength(14, ErrorMessage ="O campo CEP não pode exceder 14 caracteres")]
        public string Cnpj { get; set; }

        [StringLength(8, ErrorMessage ="O campo CEP não pode exceder 8 caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage ="O Classificação é obrigatório")]
        public int Classificacao { get; set; }

        [Required(ErrorMessage ="O E-mail é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        public List<ClienteTelefoneDto> Telefones { get; set;}
    }
}