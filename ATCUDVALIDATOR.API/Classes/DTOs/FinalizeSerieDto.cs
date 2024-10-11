using System.ComponentModel.DataAnnotations;

namespace ATCUDVALIDATOR.API.Classes.DTOs
{
    public class FinalizeSerieDto : SerieDto
    {
        [Required]
        [MinLength(8)]
        public string CodValidacaoSerie { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public string SeqUltimoDocEmitido { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Justificacao { get; set; }
    }
}
