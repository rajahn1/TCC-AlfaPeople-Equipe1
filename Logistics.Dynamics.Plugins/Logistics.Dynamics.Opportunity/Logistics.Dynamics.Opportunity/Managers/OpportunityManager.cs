using Logistics.Integrate.Opportunities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Logistics.Dynamics.Opportunity.Managers
{
    public class OpportunityManager
    {

        public void CopyOpportunityToDynamics2(Entity opportunity)
        {
            var serviceFromDynamics2 = Connection.CrmService();
            Entity oppDynamics2 = new Entity("opportunity");

            foreach (var attribute in opportunity.Attributes)
            {
                if (attribute.Value is EntityReference && ((EntityReference)attribute.Value).LogicalName == "systemuser")
                {
                    oppDynamics2[attribute.Key] = new EntityReference("systemuser", new Guid("4c9d1376-b3d0-ee11-904d-6045bd3988d4"));
                }

                else if (attribute.Value is EntityReference && ((EntityReference)attribute.Value).LogicalName == "account")
                {
                    continue;
                }

                else if (attribute.Value is EntityReference && ((EntityReference)attribute.Value).LogicalName == "contact")
                {
                    continue;
                }

                else if (attribute.Value is EntityReference && ((EntityReference)attribute.Value).LogicalName == "pricelevel")
                {
                    continue;
                }

                else if (attribute.Key == "log_integrado")
                {
                    oppDynamics2[attribute.Key] = true;
                }

                else if (attribute.Value != null)
                {
                    oppDynamics2[attribute.Key] = attribute.Value;
                }
            }

            serviceFromDynamics2.Create(oppDynamics2);
        }

        public void CreateIdForOpportunity(IOrganizationService service, Entity opportunity)
        {
            string opportunityId;

            Repository.OpportunityRepository opportunityRepository = new Repository.OpportunityRepository();
            EntityCollection opportunitiesCollection = opportunityRepository.RetrieveIDs(service);

            List<string> existentIds = new List<string>();

            foreach (var entity in opportunitiesCollection.Entities)
            {
                if (entity.Contains("log_id"))
                {
                    existentIds.Add(entity["log_id"].ToString());
                }
            }

            while (true)
            {
                Utils.IdGenerator idGenerator = new Utils.IdGenerator();
                opportunityId = idGenerator.GenerateOpportunityID();

                if (existentIds.Count > 0)
                {
                    if (!existentIds.Contains(opportunityId)) break;
                }
                else
                {
                    break;
                }
            }

            opportunity["log_id"] = opportunityId;
            service.Update(opportunity);
        }

        public void CreateOpportunity(IExecutionContext context, IOrganizationService service)
        {
            Entity opportunity = (Entity)context.InputParameters["Target"];
            if (opportunity.LogicalName != "opportunity" || context.MessageName.ToLower() != "create") return;
            try
            {
                CreateIdForOpportunity(service, opportunity);
                CopyOpportunityToDynamics2(opportunity);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
