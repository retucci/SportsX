using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsXDomain
{
    public class ClienteTelefone
    {
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(11)")]
        [Required(AllowEmptyStrings = false)] 
        public string Numero { get; set; }
        
        public int ClienteId { get; set; }

        public Cliente Cliente { get; }
    }
}