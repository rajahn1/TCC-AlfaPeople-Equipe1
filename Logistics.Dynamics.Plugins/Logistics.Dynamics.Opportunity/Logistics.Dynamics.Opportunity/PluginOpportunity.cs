using Microsoft.Xrm.Sdk;
using System;

namespace Logistics.Dynamics.Opportunity
{
    public class PluginOpportunity : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                Managers.OpportunityManager opportunityManager = new Managers.OpportunityManager();
                opportunityManager.CreateOpportunity(context, service);
            }

        }

    }
}
