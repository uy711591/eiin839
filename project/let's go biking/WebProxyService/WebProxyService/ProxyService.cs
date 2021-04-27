using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebProxyService
{
    public class ProxyService : IProxyService
    {
        CustomObjectCache<RestApi> restApiCache;

        public ProxyService()
        {
            if (restApiCache == null)
            {
                restApiCache = new CustomObjectCache<RestApi>();
            }
        }

        public string get(string uri)
        {
            return restApiCache.get(uri).getResponse();
        }
    }
}
