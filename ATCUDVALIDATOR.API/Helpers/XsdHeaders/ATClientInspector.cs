using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;

namespace ATCUDVALIDATOR.API.Helpers.XsdHeaders
{
    public class ATClientInspector(params MessageHeader[] headers) : IClientMessageInspector
    {
        public MessageHeader[] Headers { get; set; } = headers;

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (Headers != null)
            {
                for (int i = Headers.Length - 1; i >= 0; i--)
                    request.Headers.Insert(0, Headers[i]);
            }
            return request;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}
