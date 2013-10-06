using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using ServiceBusRelayCommon;

namespace ServiceBusRelayServer
{
    class ServiceBusRelayServer
    {
        static void Main(string[] args)
        {
            string serviceNamespace = "";
            string issuerName = "";
            string issuerSecret = "";

            TransportClientEndpointBehavior relayCredentials = new TransportClientEndpointBehavior();
            relayCredentials.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

            Uri address = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, "BasicMath");

            ServiceHost host = new ServiceHost(typeof(BasicMath), address);
            host.AddServiceEndpoint(typeof(IBasicMath), new NetTcpRelayBinding(), address).Behaviors.Add(relayCredentials);
            host.Open();

            Console.WriteLine("Service address: " + address);
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

            host.Close();
        }
    }
}
