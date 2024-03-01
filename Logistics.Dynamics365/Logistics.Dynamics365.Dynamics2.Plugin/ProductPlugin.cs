using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Logistics.Dynamics365.Dynamics2.Plugin
{
    public class ProductPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService (typeof(IOrganizationServiceFactory));
            IOrganizationService organizationService = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService trace = (ITracingService)serviceProvider.GetService (typeof(ITracingService));

            // Verifica se é uma operação de criação de produto
            if (context.MessageName.ToLower() == "create" && context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity product = (Entity)context.InputParameters["Target"];

                var logDynamics = product.GetAttributeValue<String>("log_dynamicsid1");
                var prodname = product.GetAttributeValue<String>("name");

                // Verifica se é a entidade de produto
                if (product.LogicalName.ToLower() == "product")
                {
                    trace.Trace("Barrando criar produto: " + prodname);

                    // Evita bloquear cadastro vindo do plugin do ambiente Dynamics 1
                    if (product.Contains("log_dynamicsid1"))
                    {
                        trace.Trace("Produto: " + prodname + " veio do dynamics 1: " + logDynamics);
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Você não tem permissão para criar produtos.");
                    }

                }
            }
        }
    }
}
