using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusRelayCommon
{
    public enum Operation
    {
        ADD, SUBTRACT, DIVIDE, MULTIPLY, REMAINDER
    }

    [ServiceBehavior(Name = "BasicMath", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
    public class BasicMath : IBasicMath
    {
        public double PerformOperation(double first, double second, Operation operation = Operation.ADD)
        {
            switch (operation)
            {
                case Operation.ADD:
                    return first + second;
                case Operation.DIVIDE:
                    if (second.Equals(0)) return 0.0;
                    return first/second;
                case Operation.MULTIPLY:
                    return first*second;
                case Operation.REMAINDER:
                    if (second.Equals(0)) return 0.0;
                    return first%second;
                case Operation.SUBTRACT:
                    return first - second;
                default:
                    return 0.0;
            }
        }
    }
}
