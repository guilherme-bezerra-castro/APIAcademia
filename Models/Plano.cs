using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIAcademia.Models;

[Table("Planos")]
public class Plano
{
    [Key]
    public int PlanoId { get; set; }

    [Required]
    [StringLength(40)]
    public string? PlanoNome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemURL { get; set; }

    [Required]
    [StringLength(400)]
    public string? Descricao { get; set; }

    public decimal Mensalidade { get; set; }

    public ICollection<Aluno>? Alunos { get; set; }
}
