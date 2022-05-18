using FCCodingChallenge.API.Data;
using FCCodingChallenge.API.Data.Enum;
using FCCodingChallenge.API.Data.Models;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Utilities
{
    public class ChannelService
    {
        
        public static bool AuthorizeChannel(RemoteDetails remoteDetails)
        {
            if (remoteDetails is null)
                return true;

            bool resp = false;
            string serviceName = "AuthorizeChannel";

            if (remoteDetails.ApiKey != "11c7479f-3d38-4ec8-9d58-2b7f22624f1b")
                return false;

            return resp = true;
        }
    }
}
