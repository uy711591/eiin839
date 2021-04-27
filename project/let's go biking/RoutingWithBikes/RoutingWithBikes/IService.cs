using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RoutingWithBike
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "closestStations?fromAddress={fromAddress}&toAddress={toAddress}")]
        List<RoutingWithBikeItinirary.Itinirary> getClosestStations(string fromAddress, string toAddress);
    }

}
