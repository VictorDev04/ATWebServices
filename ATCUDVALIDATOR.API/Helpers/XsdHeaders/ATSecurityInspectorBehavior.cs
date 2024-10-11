using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ATCUDVALIDATOR.API.Helpers.XsdHeaders
{
    public class ATSecurityInspectorBehavior(ATClientInspector clientInspector) : IEndpointBehavior
    {
        public ATClientInspector ClientInspector { get; set; } = clientInspector;

        public void Validate(ServiceEndpoint endpoint)
        { }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            if (ClientInspector == null) throw new InvalidOperationException("Caller must supply ClientInspector.");
            clientRuntime.ClientMessageInspectors.Add(ClientInspector);
        }
    }
}
