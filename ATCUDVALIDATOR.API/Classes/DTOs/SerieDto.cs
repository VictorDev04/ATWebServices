using System.ComponentModel.DataAnnotations;

namespace ATCUDVALIDATOR.API.Classes.DTOs
{
    /// <summary>
    /// View doc to more details
    /// https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao/Comunicacao_Series_ATCUD/Documents/Comunicacao_de_Series_Documentais_Manual_de_Integracao_de_SW_Aspetos_Especificos.pdf
    /// 2.1. Registar Séries
    /// </summary>
    public class SerieDto
    {
        /// <summary>
        /// case-insensitive, AB = ab = Ab... 
        /// </summary>
        [RegularExpression(@"^(?!at)[a-zA-Z0-9](?:(?![^a-zA-Z0-9]{2})[a-zA-Z0-9-._]*[a-zA-Z0-9])?$",
           ErrorMessage = "Characters are not allowed.")]
        [Required]
        [MaxLength(35)]
        public string Serie { get; set; }

        /// <summary>
        /// SI - Faturas e documentos retificativos
        /// MG - Documentos de Transporte
        /// WD - Documentos de Conferência
        /// PY - Recibos
        /// </summary>
        [Required]
        [Length(2, 2)]
        public string ClasseDoc { get; set; }

        /// <summary>
        /// Códigos para o tipo de documento
        ///<table>
        ///<tr>
        ///<th >Código o tipo documento</th>
        ///<th>Código tipo documento</th>
        ///<th>Código classe documento</th>
        ///</tr>
        ///<tr><td>FT</td> <td>Fatura </td> <td>SI</td></tr>
        ///<tr><td>FS</td> <td>Fatura simplificada</td> <td> SI</td></tr>
        ///<tr><td>FR</td> <td>Fatura-recibo</td> <td> SI</td></tr>
        ///<tr><td>ND</td> <td>Nota de débito</td> <td> SI</td></tr>
        ///<tr><td>NC</td> <td>Nota de crédito</td> <td> SI</td></tr>
        ///<tr><td>GR</td> <td>Guia de remessa</td> <td> MG</td></tr>
        ///<tr><td>GT</td> <td>Guia de transporte </td> <td>MG</td></tr>
        ///<tr><td>GA</td> <td>Guia de movimentação de ativos fixos próprios</td> <td> MG</td></tr>
        ///<tr><td>GC</td> <td>Guia de consignação </td> <td>MG</td></tr>
        ///<tr><td>GD</td> <td>Guia ou nota de devolução</td> <td> MG</td></tr>
        ///<tr><td>CM</td> <td>Consultas de mesa</td> <td> WD</td></tr>
        ///<tr><td>CC</td> <td>Crédito de consignação</td> <td> WD</td></tr>
        ///<tr><td>FC</td> <td>Fatura de consignação </td> <td>WD</td></tr>
        ///<tr><td>FO</td> <td>Folhas de obra</td> <td> WD</td></tr>
        ///<tr><td>NE</td> <td>Nota de encomenda</td> <td> WD</td></tr>
        ///<tr><td>OU</td> <td>Outros</td> <td> WD</td></tr>
        ///<tr><td>OR</td> <td>Orçamentos</td> <td> WD</td></tr>
        ///<tr><td>PF</td> <td>Pró-forma </td> <td>WD</td></tr>
        ///<tr><td>RP</td> <td>Prémio ou recibo de prémio </td> <td>WD</td></tr>
        ///<tr><td>RE</td> <td>Estorno ou recibo de estorno </td> <td>WD</td></tr>
        ///<tr><td>CS</td> <td>Imputação a cosseguradoras </td> <td>WD</td></tr>
        ///<tr><td>LD</td> <td>Imputação a cosseguradora líder </td> <td>WD</td></tr>
        ///<tr><td>RA</td> <td>Resseguro aceite </td> <td>WD</td></tr>
        ///<tr><td>RC</td> <td>Recibo emitido no âmbito do regime de IVA de Caixa</td> <td>PY</td></tr>
        ///<tr><td>RG</td> <td>Outros recibos emitidos</td> <td>PY</td></tr>
        ///</table>
        /// </summary>
        [Required]
        [Length(2, 2)]
        public string TipoDoc { get; set; }
    }
}
