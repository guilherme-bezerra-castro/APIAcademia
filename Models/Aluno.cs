using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIAcademia.Models
{
    public class Aluno // era Categoria
    {
        [Key]
        public int AlunoId { get; set; } 

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; } 

        [Required]
        [StringLength(300)]
        public string? ImagemURL { get; set; }

        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public bool Ativo {  get; set; }

        public int PlanoId { get; set; }

        [JsonIgnore]
        public Plano? Plano { get; set; }
    }
}
