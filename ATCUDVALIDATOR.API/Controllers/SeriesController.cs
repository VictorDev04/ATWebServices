using ATCUDVALIDATOR.API.Classes.DTOs;
using ATCUDVALIDATOR.API.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Saml;
using ServiceSeriesWS;
using System;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ATCUDVALIDATOR.API.Controllers
{
    /// <summary>
    /// View doc to more details
    /// https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao/Comunicacao_Series_ATCUD/Documents/Comunicacao_de_Series_Documentais_Manual_de_Integracao_de_SW_Aspetos_Especificos.pdf
    /// </summary>
    public class SeriesController(ILogger<SeriesController> logger, ServiceSeriesAt serviceSeries) : BaseController
    {
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterSerieDto serie)
        {
            return GetExecutionResult(await serviceSeries.Register(serie));
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(DeleteSerieDto serie)
        {
            return GetExecutionResult(await serviceSeries.Delete(serie));
        }

        [HttpPost("Finalize")]
        public async Task<IActionResult> Finalize(FinalizeSerieDto serie)
        {
            return GetExecutionResult(await serviceSeries.Finalize(serie));
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(SearchSerieDto serie)
        {
            return GetExecutionResult(await serviceSeries.Search(serie));
        }
    }
}
