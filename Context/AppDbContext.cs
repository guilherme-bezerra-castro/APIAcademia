using APIAcademia.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }
    public DbSet<Aluno>? Alunos { get; set; }
    public DbSet<Plano>? Planos { get; set; }
}

