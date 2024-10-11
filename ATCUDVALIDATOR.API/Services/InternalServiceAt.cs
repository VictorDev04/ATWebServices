using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Security.Authentication;
using System.Net.Http.Headers;
using ServiceSeriesWS;
using System.ServiceModel;
using ServiceMovementsDocsWS;
using System.Formats.Asn1;
using ATCUDVALIDATOR.API.Helpers.XsdHeaders;
using ATCUDVALIDATOR.API.Helpers;
using ATCUDVALIDATOR.API.Classes;
using Microsoft.Extensions.Options;
using ServiceTransportDocumentsWS;

namespace ATCUDVALIDATOR.API.Services
{
    public abstract class InternalServiceAt<TClass>(IOptions<AppSettings> config)
        where TClass : class, new()
    {
        public readonly IOptions<AppSettings> config = config;

        private static readonly List<string> successCodes =
        [
            "0",
            "2001",
            "2003",
            "2004",
            "2002",
        ];

        protected ExecutionResult<T> TreatResults<T>(operationResultInfo operationResultInfo, T resultObject)
        where T : class, new()
        {
            if (!successCodes.Contains(operationResultInfo.codResultOper))
            {
                return new ExecutionResult<T>(operationResultInfo.codResultOper, operationResultInfo.msgResultOper, false, resultObject);
            }
            return new ExecutionResult<T>(operationResultInfo.codResultOper, operationResultInfo.msgResultOper, true, resultObject);
        }

        protected ExecutionResult<T[]> TreatResults<T>(operationResultInfo operationResultInfo, T[] resultObject)
            where T : class, new()
        {
            if (!successCodes.Contains(operationResultInfo.codResultOper))
            {
                return new ExecutionResult<T[]>(operationResultInfo.codResultOper, operationResultInfo.msgResultOper, false, resultObject);
            }
            return new ExecutionResult<T[]>(operationResultInfo.codResultOper, operationResultInfo.msgResultOper, true, resultObject);
        }

        protected ExecutionResult TreatResults(operationResultInfo operationResultInfo)
        {
            if (!successCodes.Contains(operationResultInfo.codResultOper))
            {
                return new ExecutionResult(operationResultInfo.codResultOper, operationResultInfo.msgResultOper, false);
            }

            return new ExecutionResult().SuccessResult();
        }

        protected ExecutionResult<T> TreatResults<T>(ResponseStatus[] operationResultInfo, T result)
            where T : class, new()
        {
            if (!successCodes.Contains(operationResultInfo[0].ReturnCode))
            {
                return new ExecutionResult<T>(operationResultInfo[0].ReturnCode, operationResultInfo[0].ReturnMessage, false, result);
            }
            return new ExecutionResult<T>(operationResultInfo[0].ReturnCode, operationResultInfo[0].ReturnMessage, true, result);
        }

        protected TClass GetClient()
        {
            var certificate = new X509Certificate2(config.Value.CertificatePath, config.Value.CertificatePassword, X509KeyStorageFlags.DefaultKeySet);
            var publicKey = new X509Certificate2(config.Value.PublicKeyPath);
            var securityInspectorBehavior = new ATSecurityInspectorBehavior(new ATClientInspector(new ATSecurityHeader(config.Value.EFaturaUser, config.Value.EFaturaPassword, publicKey)));
            var binding = new BasicHttpsBinding();
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            binding.Security.Transport = new HttpTransportSecurity { ClientCredentialType = HttpClientCredentialType.Certificate };
            return CreateClient(binding, securityInspectorBehavior, certificate);
        }

        protected abstract TClass CreateClient(BasicHttpsBinding binding, ATSecurityInspectorBehavior securityInspectorBehavior, X509Certificate2 certificate);
    }
}
