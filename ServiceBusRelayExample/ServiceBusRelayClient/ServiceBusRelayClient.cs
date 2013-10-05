using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using ServiceBusRelayCommon;

namespace ServiceBusRelayClient
{
    class ServiceBusRelayClient
    {
        static void Main(string[] args)
        {
            string serviceNamespace = "";
            string issuerName = "";
            string issuerSecret = "";

            TransportClientEndpointBehavior relayCredentials = new TransportClientEndpointBehavior();
            relayCredentials.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, "BasicMath");
            ChannelFactory<IBasicMathChannel> channelFactory = new ChannelFactory<IBasicMathChannel>(new NetTcpRelayBinding(), new EndpointAddress(serviceUri));
            channelFactory.Endpoint.Behaviors.Add(relayCredentials);
            IBasicMathChannel channel = channelFactory.CreateChannel();
            channel.Open();

            Console.WriteLine("Press any key to exit");

            Random rnd = new Random();

            while (!Console.KeyAvailable)
            {
                double retval = channel.PerformOperation(rnd.Next(1, 40), rnd.Next(1, 40), Operation.ADD);
                Console.WriteLine("return value = {0}", retval);
            }

            channel.Close();
            channelFactory.Close();
        }
    }
}
