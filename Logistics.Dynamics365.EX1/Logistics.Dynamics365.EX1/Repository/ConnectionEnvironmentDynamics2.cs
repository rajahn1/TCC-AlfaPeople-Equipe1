using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Dynamics365.EX1.Repository
{
    class ConnectionEnvironmentDynamics2
    {
        public static IOrganizationService Service { get; set; }

        public static IOrganizationService GetService()
        {
            if (Service == null)

            {
                var user = "RETIRADOS POR MOTIVOS DE SEGURANÇA";
                var senha = "RETIRADOS POR MOTIVOS DE SEGURANÇA";
                var url = "https://org61420ec7.crm2.dynamics.com";


                CrmServiceClient crmServiceClient = new CrmServiceClient(//service dynamics 2

                    "AuthType=Office365;" +
                    $"Username={user};" +
                    $"Password={senha};" +
                    $"Url={url};"



                    );

                Service = crmServiceClient.OrganizationWebProxyClient;

            }
            return Service;
        }
    }
}
