using System.ComponentModel.DataAnnotations;

namespace SportsXWebAPI.Dto
{
    public class ClienteTelefoneDto
    {
        public int Id { get; set; }
        
        [StringLength(11, ErrorMessage ="O campo Numero não pode exceder 11 caracteres")]
        public string Numero { get; set; }     
    }
}