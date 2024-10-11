using ATCUDVALIDATOR.API.Classes;
using ATCUDVALIDATOR.API.Classes.DTOs;
using ATCUDVALIDATOR.API.Helpers;
using ATCUDVALIDATOR.API.Helpers.XsdHeaders;
using Microsoft.Extensions.Options;
using ServiceSeriesWS;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace ATCUDVALIDATOR.API.Services
{
    /// <summary>
    /// View doc to more details
    /// https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao/Comunicacao_Series_ATCUD/Documents/Comunicacao_de_Series_Documentais_Manual_de_Integracao_de_SW_Aspetos_Especificos.pdf
    /// </summary>
    public class ServiceSeriesAt(IOptions<AppSettings> config) : InternalServiceAt<SeriesWSClient>(config)
    {
        public async Task<ExecutionResult<seriesInfo>> Register(RegisterSerieDto serieDto)
        {
            var result = (await GetClient().registarSerieAsync(new registarSerieRequest
            {
                serie = serieDto.Serie,
                tipoSerie = serieDto.TipoSerie,
                classeDoc = serieDto.ClasseDoc,
                tipoDoc = serieDto.TipoDoc,
                numInicialSeq = serieDto.NumInicialSeq,
                dataInicioPrevUtiliz = serieDto.DataInicioPrevUtiliz,
                meioProcessamento = serieDto.MeioProcessamento,
                numCertSWFatur = serieDto.NumCertSWFatur
            })).registarSerieResp;

            return base.TreatResults(result.infoResultOper, result.infoSerie);
        }

        public async Task<ExecutionResult<seriesInfo>> Delete(DeleteSerieDto serieDto)
        {
            var result = (await GetClient().anularSerieAsync(new anularSerieRequest
            {
                serie = serieDto.Serie,
                classeDoc = serieDto.ClasseDoc,
                codValidacaoSerie = serieDto.CodValidacaoSerie,
                tipoDoc = serieDto.TipoDoc,
                declaracaoNaoEmissao = serieDto.DeclaracaoNaoEmissao,
                motivo = serieDto.Motivo,
            })).anularSerieResp;
            return base.TreatResults(result.infoResultOper, result.infoSerie);
        }

        public async Task<ExecutionResult<seriesInfo>> Finalize(FinalizeSerieDto serieDto)
        {
            var result = (await GetClient().finalizarSerieAsync(new finalizarSerieRequest
            {
                serie = serieDto.Serie,
                classeDoc = serieDto.ClasseDoc,
                codValidacaoSerie = serieDto.CodValidacaoSerie,
                justificacao = serieDto.Justificacao,
                seqUltimoDocEmitido = serieDto.SeqUltimoDocEmitido,
                tipoDoc = serieDto.TipoDoc
            })).finalizarSerieResp;
            return base.TreatResults(result.infoResultOper, result.infoSerie);
        }

        public async Task<ExecutionResult<seriesInfo[]>> Search(SearchSerieDto serieDto)
        {
            var result = (await GetClient().consultarSeriesAsync(new consultarSeriesRequest
            {
                classeDoc = serieDto.ClasseDoc,
                codValidacaoSerie = serieDto.CodValidacaoSerie,
                dataRegistoAte = serieDto.DataRegistoAte,
                dataRegistoDe = serieDto.DataRegistoDe,
                estado = serieDto.Estado,
                meioProcessamento = serieDto.MeioProcessamento,
                serie = serieDto.Serie,
                tipoDoc = serieDto.TipoDoc,
                tipoSerie = serieDto.TipoSerie,
            })).consultarSeriesResp;
            return base.TreatResults(result.infoResultOper, result.infoSerie);
        }

        protected override SeriesWSClient CreateClient(BasicHttpsBinding binding, ATSecurityInspectorBehavior securityInspectorBehavior, X509Certificate2 certificate)
        {
            var client = new SeriesWSClient(binding, new EndpointAddress(config.Value.WebServiceUrl.Series));
            client.ClientCredentials.ClientCertificate.Certificate = certificate;
            client.Endpoint.EndpointBehaviors.Add(securityInspectorBehavior);
            return client;
        }
    }
}
