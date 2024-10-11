namespace ATCUDVALIDATOR.API.Classes.DTOs
{
    public class SearchSerieDto 
    {
        public string Serie { get; set; }

        public string TipoSerie { get; set; }

        public string ClasseDoc { get; set; }

        public string TipoDoc { get; set; }

        public string CodValidacaoSerie { get; set; }

        public DateTime DataRegistoDe { get; set; }

        public DateTime DataRegistoAte { get; }

        public string Estado { get; set; }

        public string MeioProcessamento { get; set; }
    }
}
