// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsXDomain
{
    public class Cliente
    {
        public int Id { get; set; }

        public int Tipo { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string RazaoSocial { get; set; }

        [Column(TypeName = "varchar(11)")]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar(14)")] 
        public string Cnpj { get; set; }

        [Column(TypeName = "varchar(8)")] 
        public string Cep { get; set; }

        public int Classificacao { get; set; }

        public string Email { get; set; }

        public List<ClienteTelefone> Telefones { get; set; }
    }
}