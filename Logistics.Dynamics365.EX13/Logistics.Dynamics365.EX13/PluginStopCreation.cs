using Logistics.Dynamics365.EX13.Repository;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Dynamics365.EX13
{
    public class Class : IPlugin
    {
        void IPlugin.Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            // responsible by the messages of the plugin
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // here is the name of the metadata that have been write
            Entity target = (Entity)context.InputParameters["Target"];
            if (context.MessageName == "Create") {
                if (context.PrimaryEntityName == "contact") {
                    string cpf = target.GetAttributeValue<string>("cr192_cpf");
                    trace.Trace("here 1");
                    if (RepositoryContact.isThereAnContactWithThisCPF(service, cpf, trace))
                    {
                        trace.Trace("here 2");
                        throw new InvalidPluginExecutionException("Você não pode criar um contato com o mesmo cpf de outro contato.");
                    }
                }
                if (context.PrimaryEntityName == "account") {
                    string cnpj = target.GetAttributeValue<string>("cr192_cnpj");
                    if (RepositoryAccount.isThereAnAccountWithThisCNPJ(service, cnpj)) {
                        throw new InvalidPluginExecutionException("Você não pode criar um contato com o mesmo cnpj de outra conta.");
                    }
                    
                }
                    
            }
            if (context.MessageName == "Update")
            {
                if (context.PrimaryEntityName == "contact")
                {
                    string cpf = target.GetAttributeValue<string>("cr192_cpf");

                    if (RepositoryContact.isThereAnContactWithThisCPF(service, cpf, trace))
                    {
                        throw new InvalidPluginExecutionException("Você não pode criar um contato com o mesmo cpf de outro contato.");
                    }
                }
                if (context.PrimaryEntityName == "account")
                {
                    string cnpj = target.GetAttributeValue<string>("cr192_cnpj");
                    if (RepositoryAccount.isThereAnAccountWithThisCNPJ(service, cnpj))
                    {
                        throw new InvalidPluginExecutionException("Você não pode criar um contato com o mesmo cnpj de outra conta.");
                    }

                }

            }
        }
    }
}
