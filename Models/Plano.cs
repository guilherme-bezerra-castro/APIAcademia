using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAcademia.Models;

[Table("Planos")]
public class Plano
{
    public Plano()
    {
        Alunos = new Collection<Aluno>();
    }

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

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Mensalidade { get; set; }

    public ICollection<Aluno>? Alunos { get; set; }
}
