namespace APIAcademia.DTOs
{
    public class ResultadoPaginado<T>
    {
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
        public IEnumerable<T> Dados { get; set; } = [];
    }
}
