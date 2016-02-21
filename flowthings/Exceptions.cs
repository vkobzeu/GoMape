using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace flowthings
{
    public class FlowThingsException : Exception
    {
        public JObject responseFromServer { get; set; }

        public FlowThingsException()
        {
            this.responseFromServer = null;
        }

        public FlowThingsException(JObject responseFromServer)
        {
            this.responseFromServer = responseFromServer;
        }
    }

    public class FlowThingsBadRequestException : FlowThingsException
    {
        public FlowThingsBadRequestException(JObject responseFromServer)
            : base(responseFromServer)
        {

        }
    }

    public class FlowThingsForbiddenException : FlowThingsException
    {
        public FlowThingsForbiddenException(JObject responseFromServer)
            : base(responseFromServer)
        {

        }
    }

    public class FlowThingsNotFoundException : FlowThingsException
    {
        public FlowThingsNotFoundException(JObject responseFromServer)
            : base(responseFromServer)
        {

        }
    }

    public class FlowThingsServerErrorException : FlowThingsException
    {
        public FlowThingsServerErrorException(JObject responseFromServer)
            : base(responseFromServer)
        {

        }
    }

    public class FlowThingsNotImplementedException : Exception { }
    public class FlowThingsNotConnectedException : Exception { }
}
