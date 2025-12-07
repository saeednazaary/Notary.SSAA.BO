namespace Notary.SSAA.BO.SharedKernel.Exceptions
{
    public class ServiceException : Exception
    {
        public string ServiceName { get; }
        public Dictionary<string, object> ServiceData { get; private set; }
        public ServiceException(string serviceMessage, string message) : base(message)
        {
            ServiceMessage = serviceMessage;
        }
        public ServiceException(string serviceMessage, string message, string serviceName) : base(message)
        {
            ServiceMessage = serviceMessage;
            ServiceName = serviceName;
        }
        public ServiceException(string serviceMessage, string message, string serviceName, Dictionary<string, object> serviceData) : base(message)
        {
            ServiceMessage = serviceMessage;
            ServiceName = serviceName;
            ServiceData = serviceData;
        }
        public string ServiceMessage { get; }
    }
}