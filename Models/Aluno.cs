using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIAcademia.Models;

[Table("Alunos")]
public class Aluno
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
    public Plano? Planos { get; set; }
}
