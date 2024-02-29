using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Logistics.Dynamics365.EX13.Repository
{
    internal class RepositoryContact
    {

        public static bool isThereAnContactWithThisCPF(IOrganizationService service, string cpf,ITracingService trace)
        {
            var context = new OrganizationServiceContext(service);
            return (from contact in context.CreateQuery("contact")
                    where ((string)contact["cr192_cpf"]) == cpf
                    select contact).AsEnumerable().Count() > 0;
        }
    }
}
