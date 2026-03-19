using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAcademia.Migrations
{
    /// <inheritdoc />
    public partial class PopulaAlunos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Alunos(Nome, ImagemURL, Email, DataNascimento, Ativo, PlanoId)" + "Values('José da Silva', 'josedasilva.jpg', 'josedasilva@email.com', '1975-08-22', 1, 1)");
            mb.Sql("Insert into Alunos(Nome, ImagemURL, Email, DataNascimento, Ativo, PlanoId)" + "Values('Maria Gomes', 'mariagomes.jpg', 'mariagomes@email.com', '2005-11-02', 1, 3)");
            mb.Sql("Insert into Alunos(Nome, ImagemURL, Email, DataNascimento, Ativo, PlanoId)" + "Values('Carlos Alberto', 'carlosalberto.jpg', 'carlosalberto@email.com', '1969-05-15', 1, 3)");
            mb.Sql("Insert into Alunos(Nome, ImagemURL, Email, DataNascimento, Ativo, PlanoId)" + "Values('Antônio de Aquino', 'antoniodeaquino.jpg', 'antoniodeaquino@email.com', '1981-01-12', 1, 1)");
            mb.Sql("Insert into Alunos(Nome, ImagemURL, Email, DataNascimento, Ativo, PlanoId)" + "Values('Julia Tavares', 'juliatavares.jpg', 'juliatavares@email.com', '2002-06-29', 1, 2)");
            mb.Sql("Insert into Alunos(Nome, ImagemURL, Email, DataNascimento, Ativo, PlanoId)" + "Values('Vitor Dias', 'vitordias.jpg', 'vitordias@email.com', '1999-12-18', 0, 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Alunos");
        }
    }
}
