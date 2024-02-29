using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Dynamics365.EX13.Repository
{
    internal class RepositoryAccount
    {
        public static bool isThereAnAccountWithThisCNPJ(IOrganizationService service, string cnpj) {
            var context = new OrganizationServiceContext(service);
            return (from account in context.CreateQuery("account")
                                    where ((string)account["cr192_cnpj"]) == cnpj
                                    select account).AsEnumerable().Count() > 0;

        }
    }
}
