using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace ATCUDVALIDATOR.API.Classes.DTOs
{
    public class DeleteSerieDto : SerieDto
    {
        /// <summary>
        /// Indique o código de validação da Série,
        /// atribuído pela AT, cuja comunicação pretende
        /// anular.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string CodValidacaoSerie { get; set; }
        /// <summary>
        /// Use one of the codes below
        /// ER - Anulação por erro de registo
        /// </summary>
        [Required]
        [Length(2, 2)]
        public string Motivo { get; set; }

        /// <summary>
        /// Indicação informativa de que o sujeito passivo
        /// teve conhecimento de que não deve anular a
        /// comunicação de uma Série se já utilizou
        /// documentos emitidos com a informação da
        /// mesma.
        /// </summary>
        [Required]
        public bool DeclaracaoNaoEmissao { get; set; } = false;
    }
}
