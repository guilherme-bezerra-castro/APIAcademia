using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAcademia.Migrations
{
    /// <inheritdoc />
    public partial class PopulaPlanos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Planos(PlanoNome, ImagemURL, Descricao, Mensalidade)" + "Values('Saúde', 'saude.jpg', 'Plano que inclui atividades de musculação e artes marciais, perfeito para quem quer economizar.', 130.00)");
            mb.Sql("Insert into Planos(PlanoNome, ImagemURL, Descricao, Mensalidade)" + "Values('Saúde+', 'saudeplus.jpg', 'Além das atividades de musculação e artes marciais do plano básico, também conta com aulas de dança e aeróbicos.', 200.00)");
            mb.Sql("Insert into Planos(PlanoNome, ImagemURL, Descricao, Mensalidade)" + "Values('Master', 'master.jpg', 'Plano que inclui todas as atividades da academia, inclusive aulas de natação.', 310.00)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Planos");
        }
    }
}
