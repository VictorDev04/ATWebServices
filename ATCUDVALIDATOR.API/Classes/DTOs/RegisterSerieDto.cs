using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

namespace ATCUDVALIDATOR.API.Classes.DTOs
{

    public class RegisterSerieDto : SerieDto
    {
        /// <summary>
        /// N - Normal
        /// F - Formação
        /// R - Recuperação
        /// </summary>
        [Required]
        [Length(1, 1)]
        public string TipoSerie { get; set; }

        /// <summary>
        /// Indique o início da numeração de
        /// sequência do documento na Série.
        /// No caso de se tratar de uma série em
        /// utilização antes da obrigação de
        /// comunicação, que se pretenda manter,
        /// deve ser indicado o último número
        /// utilizado, nessa série, no momento da
        /// comunicação
        /// </summary>
        [Required]
        [Length(1, 25)]
        [Range(1, int.MaxValue)]
        public string NumInicialSeq { get; set; }

        /// <summary>
        /// Indique a data a partir da qual se prevê a
        /// utilização da Série.
        /// </summary>
        [Required]
        public DateTime DataInicioPrevUtiliz { get; set; }

        /// <summary>
        /// Indique o número do certificado do
        /// programa de faturação, atribuído pela AT.
        /// Se não aplicável, deve ser preenchido com
        /// “0” (zero).
        /// </summary>
        [Required]
        [Length(1, 4)]
        public string NumCertSWFatur { get; set; }

        /// <summary>
        /// Código de meio de processamento dos  documentos a emitir.
        /// PI - Programa Informático de Faturação
        /// OM - Outros Meios Eletrónicos
        /// PF - Portal das Finanças
        /// </summary>
        [Required]
        [Length(2, 2)]
        public string MeioProcessamento { get; set; }
    }
}
