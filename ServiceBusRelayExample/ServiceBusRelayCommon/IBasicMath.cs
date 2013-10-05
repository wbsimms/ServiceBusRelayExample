using System.ServiceModel;

namespace ServiceBusRelayCommon
{

    [ServiceContract(Name = "IBasicMath", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
    public interface IBasicMath
    {
        [OperationContract(IsOneWay = false)]
        double PerformOperation(double first, double second, Operation operation);
    }

    public interface IBasicMathChannel : IBasicMath, IClientChannel { }

}