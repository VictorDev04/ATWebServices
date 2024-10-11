using ATCUDVALIDATOR.API.Helpers;
using ATCUDVALIDATOR.API.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Saml;
using ServiceSeriesWS;
using System;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ATCUDVALIDATOR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController()
        {
        }

        protected IActionResult GetExecutionResult(ExecutionResult executionResult)
        {
            if (!executionResult.Success)
            {
                return this.BadRequest(executionResult.Message);
            }
            return this.Ok(executionResult.Message);
        }

        protected IActionResult GetExecutionResult<T>(ExecutionResult<T> executionResult)
        {
            if (!executionResult.Success)
            {
                return this.BadRequest(executionResult.Message);
            }
            return this.Ok(executionResult.Result);
        }
    }
}
