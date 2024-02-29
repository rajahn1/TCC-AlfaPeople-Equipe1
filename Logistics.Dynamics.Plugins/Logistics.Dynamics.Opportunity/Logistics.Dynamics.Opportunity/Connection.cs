using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Logistics.Integrate.Opportunities
{
    public class Connection
    {
        public static IOrganizationService Service { get; set; }
        public static IOrganizationService CrmService()
        {
            const string url = "https://org61420ec7.crm2.dynamics.com";
            const string user = "Rafael@LogisticsAlfapeople494.onmicrosoft.com";
            const string password = "Cpx2020@";

            CrmServiceClient crmServiceClient = new CrmServiceClient(
                "AuthType=Office365;" +
                $"Username={user};" +
                $"Password={password};" +
                $"Url={url};"
                );

            Service = crmServiceClient.OrganizationWebProxyClient;

            return Service;
        }
    }
}
